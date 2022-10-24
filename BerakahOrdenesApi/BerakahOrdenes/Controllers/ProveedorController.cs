using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BerakahOrdenes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ProveedorController(IUsuarioRepository usuarioRepository, IProveedorRepository proveedorRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _proveedorRepository = proveedorRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearProveedor(ProveedorDto proveedorDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 10);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Agregar == false)
            {
                return Ok("No puedes hacer esta accion");
            }


            if (proveedorDto == null)
            {
                return Ok("Todos los datos son requeridos");
            }

            if (_proveedorRepository.ExisteProveedorName(proveedorDto.ProveedorNombre))
            {
                return Ok("Ya existe un proveedor con este nombre ");
            }
            var provedor = _mapper.Map<Proveedor>(proveedorDto);
            provedor.ProveedorFechaCreacion = DateTime.Now;

            if (!_proveedorRepository.CrearProveedor(provedor))
            {
                return Ok("Algo salio mal guardando el registro");
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetProveedores()
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 10);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var listaProveedores = _proveedorRepository.GetProveedores();
            var listaProveedoresDto = new List<ProveedorDto>();

            foreach (var lista in listaProveedores)
            {
                listaProveedoresDto.Add(_mapper.Map<ProveedorDto>(lista));
            }
            return Ok(listaProveedoresDto);
        }

        [HttpGet("{proveedorId:int}", Name = "GetProveedor")]
        public IActionResult GetProveedor(int proveedorId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 10);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var itemProveedor = _proveedorRepository.GetProveedor(proveedorId);

            if (itemProveedor == null)
            {
                return NotFound();
            }

            var itemProveedorDto = _mapper.Map<ProveedorDto>(itemProveedor);
            return Ok(itemProveedorDto);
        }


        [HttpPut("{proveedorId:int}", Name = "ActualizarProveedor")]
        public IActionResult ActualizarProveedor(int proveedorId, [FromBody]ProveedorDto proveedorDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 10);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Modificar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (proveedorDto == null || proveedorId != proveedorDto.ProveedorId)
            {
                return BadRequest(ModelState);
            }

            var proveedor = _proveedorRepository.GetProveedor(proveedorDto.ProveedorId);
            if (proveedor == null)
            {
                return Ok("El proveedor ya no existe");
            }

            proveedor.ProveedorNombre = proveedorDto.ProveedorNombre;
            proveedor.ProveedorTelefono = proveedorDto.ProveedorTelefono;
            proveedor.ProveedorCorreo = proveedorDto.ProveedorCorreo;
            proveedor.ProveedorNit = proveedorDto.ProveedorNit;
            proveedor.ProveedorDireccion = proveedorDto.ProveedorDireccion;



            if (!_proveedorRepository.ActualizarProveedor(proveedor))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return Ok(ModelState);
            }
            return Ok(1);
        }

        [HttpPut("BorrarProveedor")]
        public IActionResult BorrarProveedor(ProveedorActualizarDto proveedorId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 10);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Eliminar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var proveedor = _proveedorRepository.GetProveedor(proveedorId.ProveedorId);

            if(proveedor == null || proveedor.ProveedorEstado == false)
            {
                Ok("El proveedor no exise");
            }

            proveedor.ProveedorEstado = false;

            if (!_proveedorRepository.ActualizarProveedor(proveedor))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro");
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
