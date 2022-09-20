using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuario= _usuarioRepository.Login(usuarioLoginDto.Usuario, usuarioLoginDto.UsuarioPass);

            if (usuario == null)
            {
                return Ok("Unauthorized");
            }
            
            _usuarioRepository.ActualizarFechaSesionUsuario(usuario);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.UsuarioUsuario.ToString())

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

        [AllowAnonymous]
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
                UsuarioFechaSesion = DateTime.Now,
                UsaurioFechaCreacion = DateTime.Now,
            };

            if(_usuarioRepository.Registro(usuarioACrear, usuarioAuthDto.UsuarioPass) == false){

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

            return Ok(menusUsuario);
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

        [HttpPut("{usuarioId:int}", Name = "ActualizarUsuario")]
        public IActionResult ActualizarUsuario(int usuarioId, [FromBody] UsuarioActualizarDto usuarioDto)
        {
            if (usuarioDto == null || usuarioId != usuarioDto.UsuarioId)
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
    }
}
