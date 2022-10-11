using AutoMapper;
using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly DBOrdenes _db;
       

        public UsuarioController(DBOrdenes db, IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration config)
        {
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
                UsuarioNombre = usuarioAuthDto.Nombre,
                UsuarioApellido = usuarioAuthDto.Apellido,
                UsuarioCorreo = usuarioAuthDto.Correo,
                UsuarioTelefono = usuarioAuthDto.Telefono,
                UsuarioEstado = usuarioAuthDto.Estado,
                UsuarioCambioPass = true,
                UsuarioUsuarioId = UsuarioAutenticado(),
                UsuarioFechaSesion = DateTime.Now,
                UsuarioFechaCambioPass = DateTime.Now,
                UsaurioFechaCreacion = DateTime.Now,
            };
            

            if (_usuarioRepository.Registro(usuarioACrear, usuarioAuthDto.UsuarioPass) == false){

                return Ok("Error al ingresar usuario");
            }

            return Ok(1);
        }

        [HttpGet("MenusPorUsuario")]
        public IActionResult ObtenerMenusUsuario()
        {
            var claims = User.Claims.ToList();
            var usuario = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            int usuarioId = Int32.Parse(usuario);

            var menusUsuario = _usuarioRepository.ObtenerMenusUsuario(usuarioId);
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
            _usuarioRepository.ActualizarPassword(usuario, usuarioDto.PasswordNueva);


            if (!_usuarioRepository.ActualizarUsuario(usuario))
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
    }
}
