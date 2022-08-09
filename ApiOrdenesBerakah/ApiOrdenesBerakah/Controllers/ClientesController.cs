using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using ApiOrdenesBerakah.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiOrdenesBerakah.Controllers
{
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _ctRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ClientesController(IClienteRepository ctRepo, IMapper mapper, IConfiguration config)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
            _config = config;
        }


        [HttpGet]
        public ActionResult GetClientes()
        {
            var listaUsuarios = _ctRepo.GetUsuarios();

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
            var itemUsuario = _ctRepo.GetUsuario(usuarioId);

            if (itemUsuario == null)
            {
                return NotFound();
            }

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);
            return Ok(itemUsuarioDto);
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteUserName(usuarioDto.userName))
            {

                ModelState.AddModelError("", "Este User Name ya esta siendo utilizado");
                return StatusCode(404, ModelState);

            }else if (_ctRepo.ExisteCorreo(usuarioDto.correo))
            {
                ModelState.AddModelError("", "Este Corre ya fue registrado por un usuario");
                return StatusCode(404, ModelState);
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            if (!_ctRepo.CrearUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro{usuario.nombre}");
                return StatusCode(500, ModelState);
            }

            
            return CreatedAtRoute("GetUsuario", new { usuarioId = usuario.idUsuario }, usuario);
        }

        [HttpPatch("{usuarioId:int}", Name = "ActualizarUsuario")]
        public IActionResult ActualizarUsuario(int usuarioId, [FromBody] UsuarioDto usuarioDto)
        {
            if (usuarioDto == null || usuarioId != usuarioDto.idUsuario)
            {
                return BadRequest(ModelState);
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            if (!_ctRepo.ActualizarUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{usuario.nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{usuarioId:int}", Name = "BorrarUsuario")]
        public IActionResult BorrarUsuario(int usuarioId)
        {

            if (!_ctRepo.ExisteUsuario(usuarioId))
            {
                return NotFound();
            }

            var usuario = _ctRepo.GetUsuario(usuarioId);

            if (!_ctRepo.BorrarUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{usuario.nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost("Registro")]
        public IActionResult Registro(UsuarioAuthDto usuarioAuthDto)
        {
            usuarioAuthDto.userName = usuarioAuthDto.userName.ToLower();

            if (_ctRepo.ExisteUserName(usuarioAuthDto.userName))
            {
                return BadRequest("El usuario ya existe");
            }

            var usuariACrear = new Usuario
            {
                userName = usuarioAuthDto.userName
            };

            var usuarioCreado = _ctRepo.Registro(usuariACrear, usuarioAuthDto.userPass);
            return Ok(usuarioCreado);
        }

        [HttpPost("Login")]
        public IActionResult Login(UsuarioAuthLoginDto usuarioAuthLoginDto)
        {
            var usuarioDesdeRepo = _ctRepo.Login(usuarioAuthLoginDto.userName, usuarioAuthLoginDto.userPass);
            
            if(usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.idUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuarioDesdeRepo.userName.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            }); 
        }





    }



}
