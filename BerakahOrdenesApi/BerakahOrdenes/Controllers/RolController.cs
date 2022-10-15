using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BerakahOrdenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public RolController(IRolRepository rolRepository, IMapper mapper, IConfiguration config)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearRol(RolDto rolDto)
        {
            if (rolDto == null)
            {
                return BadRequest(ModelState);
            }

            var rol = _mapper.Map<Rol>(rolDto);
            rol.RolFechaCreacion = DateTime.Now;

            if (!_rolRepository.CrearRol(rol))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro{rol.RolNombre}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        [HttpGet]
        public ActionResult GetRol()
        {
            var listaRoles = _rolRepository.GetRoles();
            var listaRolesDto = new List<RolDto>();

            foreach (var lista in listaRoles)
            {
                listaRolesDto.Add(_mapper.Map<RolDto>(lista));
            }
            return Ok(listaRolesDto);
        }

        [HttpGet("{rolId:int}", Name = "GetRol")]
        public IActionResult GetRol(int rolId)
        {
            var itemRol = _rolRepository.GetRol(rolId);

            if (itemRol == null)
            {
                return NotFound();
            }

            var itemRolDto = _mapper.Map<RolDto>(itemRol);
            return Ok(itemRolDto);
        }

        [HttpPatch("{rolId:int}", Name = "ActualizarRol")]
        public IActionResult ActualizarRol(int rolId, [FromBody]RolDto rolDto)
        {
            if (rolDto == null || rolId != rolDto.RoloId)
            {
                return BadRequest(ModelState);
            }

            var rol = _mapper.Map<Rol>(rolDto);

            if (!_rolRepository.ActualizarRol(rol))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{rol.RolNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{rolId:int}", Name = "BorrarRol")]
        public IActionResult BorrarRol(int rolId)
        {

            if (!_rolRepository.ExisteRol(rolId))
            {
                return NotFound();
            }

            var rol = _rolRepository.GetRol(rolId);

            if (!_rolRepository.BorrarRol(rol))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{rol.RolNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
