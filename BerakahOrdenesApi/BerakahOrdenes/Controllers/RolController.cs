using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BerakahOrdenes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public RolController(IUsuarioRepository usuarioRepository, IRolRepository rolRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearRol(RolDto rolDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 13);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Agregar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (rolDto == null)
            {
                return BadRequest(ModelState);
            }

            var rol = _mapper.Map<Rol>(rolDto);
            rol.RolFechaCreacion = DateTime.Now;

            if (!_rolRepository.CrearRol(rol))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro{rol.RolNombre}");
                return Ok(2);
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetRol()
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 13);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

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
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 13);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var itemRol = _rolRepository.GetRol(rolId);

            if (itemRol == null)
            {
                return NotFound();
            }

            var itemRolDto = _mapper.Map<RolDto>(itemRol);
            return Ok(itemRolDto);
        }

        [HttpPut("{rolId:int}", Name = "ActualizarRol")]
        public IActionResult ActualizarRol(int rolId, [FromBody]RolDto rolDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 13);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Modificar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (rolDto == null || rolId != rolDto.RolId)
            {
                return Ok(2);
            }

            var rol = _rolRepository.GetRol(rolDto.RolId);
            if (rol == null)
            {
                return Ok("El producto ya no existe");
            }

            rol.RolNombre = rolDto.RolNombre;
            rol.RolDescripcion = rolDto.RolDescripcion;

            if (!_rolRepository.ActualizarRol(rol))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return Ok(2);
            }
            return Ok(1);
        }

        [HttpPut("BorrarRol")]
        public IActionResult BorrarRol(RolActualizarDto rolId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 13);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Eliminar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var rol = _rolRepository.GetRol(rolId.RolId);

            if(rol == null || rol.RolEstado == false)
            {
                Ok("El Rol no existe");
            }

            rol.RolEstado = false;

            if (!_rolRepository.BorrarRol(rol))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{rol.RolNombre}");
                return Ok(ModelState);
            }
            return Ok(1);
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
