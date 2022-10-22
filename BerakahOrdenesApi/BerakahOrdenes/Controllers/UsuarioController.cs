using AutoMapper;
using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace BerakahOrdenes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioRolRepository _usuarioRolRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly DBOrdenes _db;
       

        public UsuarioController(DBOrdenes db, ITokenRepository tokenRepository, IUsuarioRolRepository usuarioRolRepository, IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration config)
        {
            _tokenRepository = tokenRepository;
            _usuarioRolRepository = usuarioRolRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _config = config;
            _db = db;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (!_usuarioRepository.ExisteUserName(usuarioLoginDto.Usuario))
            {
                return Ok("Credenciales incorrectas");
            }

            var usuario = _usuarioRepository.Login(usuarioLoginDto.Usuario, usuarioLoginDto.UsuarioPass);
            
            if (usuario != null)
            {
                if (usuario.UsuarioEstado == false)
                {
                    return Ok("Contacte con su administrador en caso no pueda iniciar sesión");
                }

                if (usuario.UsuarioFechaCambioPass < DateTime.Now.AddDays(-30))
                {
                    usuario.UsuarioCambioPass = true;
                }

                usuario.UsuarioIntentos = 0;
                _usuarioRepository.ActualizarFechaSesionUsuario(usuario);

                var itemUsuarioDto = _mapper.Map<UsuarioPerfilDto>(usuario);

                InicioSesion InicioSesion = new InicioSesion();
                InicioSesion.Usuario = itemUsuarioDto;
                InicioSesion.token = CreacionTokenYClaims(usuario.UsuarioId, usuario.UsuarioNombre).Value;

                return Ok(InicioSesion);

            }

            
            int MaxIntentos = 3;
            var usuarioIntentos = _db.Usuario.FirstOrDefault(x => x.UsuarioUsuario.Equals(usuarioLoginDto.Usuario));
            if (usuarioIntentos.UsuarioEstado == false)
            {
                return Ok("Contacte con su administrador en caso no pueda iniciar sesión");
            }
            if (usuarioIntentos.UsuarioIntentos == MaxIntentos)
            {
                usuarioIntentos.UsuarioEstado = false;
                usuarioIntentos.UsuarioIntentos = 0;
                _db.Usuario.Update(usuarioIntentos);
                _db.SaveChanges();
                return Ok("Contacte con su administrador en caso no pueda iniciar sesión");

            }

            usuarioIntentos.UsuarioIntentos += 1;
            _db.Usuario.Update(usuarioIntentos);
            _db.SaveChanges();
            return Ok("Podria deshabilitar la cuenta si sigue con intentos fallidos");
        }

        
        [HttpPost("Registro")]
        public IActionResult Registro(UsuarioAuthDto usuarioAuthDto)
        {
            usuarioAuthDto.Usuario = usuarioAuthDto.Usuario.ToLower();

            if (_usuarioRepository.ExisteUserName(usuarioAuthDto.Usuario))
            {
                return Ok("El usuario ya existe");
            }

            var usuarioACrear = new Usuario
            {
                UsuarioUsuario = usuarioAuthDto.Usuario,
                UsuarioRolId = usuarioAuthDto.UsuarioRolId,
                UsuarioNombre = usuarioAuthDto.Nombre,
                UsuarioApellido = usuarioAuthDto.Apellido,
                UsuarioCorreo = usuarioAuthDto.Correo,
                UsuarioTelefono = usuarioAuthDto.Telefono,
                UsuarioEstado = true,
                UsuarioCambioPass = true,
                UsuarioUsuarioId = UsuarioAutenticado(),
                UsuarioFechaSesion = DateTime.Now,
                UsuarioFechaCambioPass = DateTime.Now,
                UsaurioFechaCreacion = DateTime.Now,
            };

            


            if (_usuarioRepository.Registro(usuarioACrear, usuarioAuthDto.UsuarioPass) == false){

                return Ok("Error al ingresar usuario");
            }

            var usuarioRol = new UsuarioRol
            {
                UsuarioId = usuarioACrear.UsuarioId,
                RolId = 1,
                UsuarioRolEstado = true,
                UsuarioRolFechaCreacion = DateTime.Now
            };

            if (!_usuarioRolRepository.CrearUsuarioRol(usuarioRol))
            {

                return Ok("Error al ingresar usuario");
            }

            return Ok(1);
        }

        [HttpGet("MenusPorUsuario")]
        public IActionResult ObtenerMenusUsuario()
        {
            var itemUsuario = _usuarioRepository.GetUsuario(UsuarioAutenticado());
            if (itemUsuario == null)
            {
                return Ok(2);
            }

            var menusUsuario = _usuarioRepository.ObtenerMenusUsuario(itemUsuario.UsuarioId, itemUsuario.UsuarioRolId);
            var listaMenus = new List<MenuDto>();

            foreach (var lista in menusUsuario)
            {
                listaMenus.Add(_mapper.Map<MenuDto>(lista));
            }

            return Ok(listaMenus);
        }

        [HttpGet]
        public ActionResult GetUsuarios()
        {
            var listaUsuarios = _usuarioRepository.GetUsuarios();
            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }
            return Ok(listaUsuariosDto);
        }

        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _usuarioRepository.GetUsuario(usuarioId);

            if (itemUsuario == null)
            {
                return Ok("El usuario no fue encontrado");
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        [HttpPut("ActualizarUsuario")]
        public IActionResult ActualizarUsuario(UsuarioActualizarDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return Ok("Un error a ocurruido");
            }

            var usuario = _usuarioRepository.GetUsuario(usuarioDto.UsuarioId);
            if (usuario == null)
            {
                return Ok("Usuario ya no existe");
            }

            usuario.UsuarioUsuario = usuarioDto.UsuarioUsuario;
            usuario.UsuarioNombre = usuarioDto.UsuarioNombre;
            usuario.UsuarioNombre = usuarioDto.UsuarioNombre;
            usuario.UsuarioCorreo = usuarioDto.UsuarioCorreo;
            usuario.UsuarioTelefono = usuarioDto.UsuarioTelefono;
            usuario.UsuarioRolId = usuarioDto.UsuarioRolId;

            if (!_usuarioRepository.ActualizarUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{usuario.UsuarioNombre}");
                return Ok(ModelState);
            }

            return Ok(1);
        }

        [HttpPut("ActualizarPassword")]
        public IActionResult CambioPassword(UsuarioPasswordDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return Ok("Es necesario que ingreses una contraseña nueva");
            }

            var usuario = _usuarioRepository.GetUsuario(UsuarioAutenticado());

            if (usuario == null || usuario.UsuarioEstado == false)
            {
                return Ok("Usuario invalido");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMininChar = new Regex(@"[a-z]+");
            var especialCaracter = new Regex(@"[!""#$%&'()*+,-./:;<=>?@\[\]\^_`{|}~]+");
            var hasMinimumChars = new Regex(@".{8,15}");

            bool isValidated = hasNumber.IsMatch(usuarioDto.PasswordNueva) && hasUpperChar.IsMatch(usuarioDto.PasswordNueva)
                                && hasMinimumChars.IsMatch(usuarioDto.PasswordNueva) && hasMininChar.IsMatch(usuarioDto.PasswordNueva)
                                && especialCaracter.IsMatch(usuarioDto.PasswordNueva);

            if (isValidated == false)
            {
                return Ok("la contraseña no cumple con los estandares de seguridad");
            }

            usuario.UsuarioCambioPass = false;
            usuario.UsuarioFechaCambioPass = DateTime.Now;
            


            if (!_usuarioRepository.ActualizarPassword(usuario, usuarioDto.PasswordNueva))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{usuario.UsuarioNombre}");
                return Ok(ModelState);
            }

            return Ok(1);
        }

        [HttpPut("ActualizarPasswordUsuario")]
        public IActionResult CambioPasswordUsuario(UsuarioPasswordDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return Ok("Es necesario que ingreses una contraseña nueva");
            }

            var usuario = _usuarioRepository.GetUsuario(UsuarioAutenticado());

            if (usuario == null || usuario.UsuarioEstado == false)
            {
                return Ok("Usuario invalido");
            }

            if (_usuarioRepository.Login(usuario.UsuarioUsuario, usuarioDto.PasswordVieja) == null)
            {
                return Ok("La contraseña actual no coincide");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMininChar = new Regex(@"[a-z]+");
            var especialCaracter = new Regex(@"[!""#$%&'()*+,-./:;<=>?@\[\]\^_`{|}~]+");
            var hasMinimumChars = new Regex(@".{8,15}");

            bool isValidated = hasNumber.IsMatch(usuarioDto.PasswordNueva) && hasUpperChar.IsMatch(usuarioDto.PasswordNueva)
                                && hasMinimumChars.IsMatch(usuarioDto.PasswordNueva) && hasMininChar.IsMatch(usuarioDto.PasswordNueva)
                                && especialCaracter.IsMatch(usuarioDto.PasswordNueva);

            if (isValidated == false)
            {
                return Ok("la contraseña no cumple con los estandares de seguridad");
            }

            usuario.UsuarioCambioPass = false;
            usuario.UsuarioFechaCambioPass = DateTime.Now;



            if (!_usuarioRepository.ActualizarPassword(usuario, usuarioDto.PasswordNueva))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{usuario.UsuarioNombre}");
                return Ok(ModelState);
            }

            return Ok(1);
        }


        [HttpDelete("{usuarioId:int}", Name = "BorrarUsuario")]
        public IActionResult BorrarUsuario(int usuarioId)
        {

            if (!_usuarioRepository.ExisteUsuario(usuarioId))
            {
                return NotFound();
            }

            var usuario = _usuarioRepository.GetUsuario(usuarioId);

            if (!_usuarioRepository.BorrarUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{usuario.UsuarioNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("RecuperarEmail")]
        public IActionResult RecuperarEmail(UsuarioObtener usuarioAuthDto)
        {
            if (usuarioAuthDto.Usuario == "")
            {
                return Ok("Debe ingresar un usuario valido");
            }

            usuarioAuthDto.Usuario = usuarioAuthDto.Usuario.ToLower();

            var usuario = _usuarioRepository.ExisteUserName2(usuarioAuthDto.Usuario);
            if (usuario == null)
            {
                return Ok("El usuario no existe");
            }

           
            string codigo = CodigoPassword();

            var token = new Token();
            token.CodigoSeguridad = codigo;
            token.UsuarioId = usuario.UsuarioId;
            token.TokenFechaCreacion = DateTime.Now;
            token.TokenEstado = true;

            if(!_tokenRepository.CrearToken(token))
            {
                Ok("Error al crear codigo, contacte con su supervisor");
            }
            
            string fromMail = "soporte.mberakah@gmail.com";
            string fromPassword = "swscuuwrduowxbiq";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Cambio de contraseña";
            message.To.Add(new MailAddress(usuario.UsuarioCorreo));
            message.Body = CuerpoCorrep(codigo, usuario.UsuarioNombre, usuario.UsuarioApellido);
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

            return Ok(1);
        }

        [AllowAnonymous]
        [HttpPost("RecuperarPassword")]
        public IActionResult RecuperarPassword(UsuarioRecuperarPasswordDto usuarioDto)
        {
            if (usuarioDto.Codigo == null)
            {
                return Ok("Es necesario que ingrese un Codigo de Seguridad");
            } 

            if(usuarioDto.Password == null){
                return Ok("Es necesario que ingrese una contraseña");
            }


            var token = _tokenRepository.GetToken(usuarioDto.Codigo);
            var usuario = token.Usuario;

            if (usuario == null)
            {
                return Ok("El usuario no existe");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMininChar = new Regex(@"[a-z]+");
            var especialCaracter = new Regex(@"[!""#$%&'()*+,-./:;<=>?@\[\]\^_`{|}~]+");
            var hasMinimumChars = new Regex(@".{8,15}");

            bool isValidated = hasNumber.IsMatch(usuarioDto.Password) && hasUpperChar.IsMatch(usuarioDto.Password)
                                && hasMinimumChars.IsMatch(usuarioDto.Password) && hasMininChar.IsMatch(usuarioDto.Password)
                                && especialCaracter.IsMatch(usuarioDto.Password);

            if (isValidated == false)
            {
                return Ok("la contraseña no cumple con los estandares de seguridad");
            }

            usuario.UsuarioCambioPass = false;
            usuario.UsuarioFechaCambioPass = DateTime.Now;



            if (!_usuarioRepository.ActualizarPassword(usuario, usuarioDto.Password))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{usuario.UsuarioNombre}");
                return Ok(ModelState);
            }

            token.TokenEstado = false;
            _tokenRepository.ActualizarToken(token);

            return Ok(1);
        }

        private OkObjectResult CreacionTokenYClaims(int usuarioId, string usuarioNombre)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuarioNombre.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }


        private int UsuarioAutenticado()
        {
            var claims = User.Claims.ToList();
            var usuario = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            int usuarioId = Int32.Parse(usuario);
            return usuarioId;
        }

        private string CodigoPassword()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            string seed = justNumbers.Substring(0, 10);
            return seed;
        }

        private string CuerpoCorrep(string codigo, string nombre, string apellido)
        {
            string stCuerpoHTML = "<!DOCTYPE html>";
            stCuerpoHTML += "<html lang='es'>";
            stCuerpoHTML += "<head>";
            stCuerpoHTML += "<meta charset='utf - 8'>";
            stCuerpoHTML += "<title>Recuperacion de correo</title>";
            stCuerpoHTML += "</head>";
            stCuerpoHTML += "<body style='background - color: black '>";
            stCuerpoHTML += "<table style='max - width: 600px; padding: 10px; margin: 0 auto; border - collapse: collapse; '>	";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td style='padding: 0'>";
            stCuerpoHTML += "<img style='padding: 0; display: block' src='cid:Fondo' width='100%' height='10%'>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "<tr>";
            stCuerpoHTML += "<td style='background - color: #ecf0f1'>";
            stCuerpoHTML += "<div style='color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>";
            stCuerpoHTML += "<h2 style='color: #e67e22; margin: 0 0 7px'>Hola " + nombre + " " + apellido + "</h2>";
            stCuerpoHTML += "<p style='margin: 2px; font - size: 15px'>";
            stCuerpoHTML += "Hemos recibido una solicitud para restablecer el password de su cuenta asociada con ";
            stCuerpoHTML += "esta dirección de correo electrónico. Si no ha realizado esta solicitud, puede ignorar este ";
            stCuerpoHTML += "correo electrónico y le garantizamos que su cuenta es completamente segura.";
            stCuerpoHTML += "<br/>";
            stCuerpoHTML += "<br/>";
            stCuerpoHTML += "El código de seguridad para cambiar su password es: " + codigo;
            stCuerpoHTML += "<br/>";
            stCuerpoHTML += "<br/>";
            stCuerpoHTML += "Atentamente: Soporte de Berakah";
            stCuerpoHTML += "</p>";
            stCuerpoHTML += "<p style='color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0'>Copyright © Multingocios Berakah 2022</p>";
            stCuerpoHTML += "</div>";
            stCuerpoHTML += "</td>";
            stCuerpoHTML += "</tr>";
            stCuerpoHTML += "</table>";
            stCuerpoHTML += "</body>";
            stCuerpoHTML += "</html>";

            return stCuerpoHTML;
        }
    }
}
