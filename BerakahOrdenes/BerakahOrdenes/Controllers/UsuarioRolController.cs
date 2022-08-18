using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BerakahOrdenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuarioRolRepository _usuarioRolRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuarioRolController(IUsuarioRolRepository usuarioRolRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRolRepository = usuarioRolRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearUsuarioRol(UsuarioRolDto usuarioRolDto)
        {
            if (usuarioRolDto == null)
            {
                return BadRequest(ModelState);
            }

            var usuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);
            usuarioRol.UsuarioRolFechaCreacion = DateTime.Now;

            if (!_usuarioRolRepository.CrearUsuarioRol(usuarioRol))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        [HttpGet]
        public ActionResult GetUsuarioRol()
        {
            var listaUsuarioRol = _usuarioRolRepository.GetUsuarioRoles();
            var listaUsuarioRolDto = new List<UsuarioRol>();

            foreach (var lista in listaUsuarioRol)
            {
                listaUsuarioRolDto.Add(_mapper.Map<UsuarioRol>(lista));
            }
            return Ok(listaUsuarioRolDto);
        }

        [HttpGet("{usuarioRolId:int}", Name = "GetUsuarioRol")]
        public IActionResult GetUsuarioRol(int usuarioRolId)
        {
            var itemUsuarioRol = _usuarioRolRepository.GetUsuarioRol(usuarioRolId);

            if (itemUsuarioRol == null)
            {
                return NotFound();
            }

            var itemUsuarioRolDto = _mapper.Map<UsuarioRol>(itemUsuarioRol);
            return Ok(itemUsuarioRolDto);
        }

        [HttpPatch("{usuarioRolId:int}", Name = "ActualizarUsuarioRol")]
        public IActionResult ActualizarUsuarioRol(int usuarioRolId, [FromBody]UsuarioRolDto usuarioRolDto)
        {
            if (usuarioRolDto == null || usuarioRolId != usuarioRolDto.UsuarioRolId)
            {
                return BadRequest(ModelState);
            }

            var usuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);

            if (!_usuarioRolRepository.ActualizarUsuarioRol(usuarioRol))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{usuarioRolId:int}", Name = "BorrarUsuarioRol")]
        public IActionResult BorrarUsuarioRol(int usuarioRolId)
        {

            if (!_usuarioRolRepository.ExisteUsuarioRol(usuarioRolId))
            {
                return NotFound();
            }

            var usuarioRol = _usuarioRolRepository.GetUsuarioRol(usuarioRolId);

            if (!_usuarioRolRepository.BorrarUsuarioRol(usuarioRol))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
