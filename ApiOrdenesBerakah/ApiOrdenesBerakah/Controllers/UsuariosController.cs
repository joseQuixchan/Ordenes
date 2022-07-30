using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using ApiOrdenesBerakah.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiOrdenesBerakah.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _ctRepo;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepository ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult GetUsuarios()
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
    }

      

}
