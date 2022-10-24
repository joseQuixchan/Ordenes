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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ProductoController(IUsuarioRepository usuarioRepository, IProductoRepository productoRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearProducto(ProductoDto productoDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 11);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Agregar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (productoDto == null)
            {
                return Ok("Todos los datos son requeridos");
            }

            if (_productoRepository.ExisteProductoName(productoDto.ProductoNombre))
            {
                return Ok("Ya existe un proveedor con este nombre ");
            }
            var producto = _mapper.Map<Producto>(productoDto);
            producto.ProductoFechaCreacion = DateTime.Now;

            if (!_productoRepository.CrearProducto(producto))
            {
                return Ok("Algo salio mal guardando el registro");
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetProductos()
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 11);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var listaProductos = _productoRepository.GetProductos();
            var listaProductosDto = new List<ProductoDto>();

            foreach (var lista in listaProductos)
            {
                listaProductosDto.Add(_mapper.Map<ProductoDto>(lista));
            }
            return Ok(listaProductosDto);
        }

        [HttpGet("{productoId:int}", Name = "GetProducto")]
        public IActionResult GetProducto(int productoId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 11);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }
            var itemProducto = _productoRepository.GetProducto(productoId);

            if (itemProducto == null)
            {
                return NotFound();
            }

            var itemProductoDto = _mapper.Map<ProductoDto>(itemProducto);
            return Ok(itemProductoDto);
        }


        [HttpPut("{productoId:int}", Name = "ActualizarProducto")]
        public IActionResult ActualizarProducto(int productoId, [FromBody] ProductoDto productoDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 11);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Modificar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (productoDto == null || productoId != productoDto.ProductoId)
            {
                return BadRequest(ModelState);
            }

            var producto = _productoRepository.GetProducto(productoDto.ProductoId);
            if (producto == null)
            {
                return Ok("El producto ya no existe");
            }

            producto.ProductoNombre = productoDto.ProductoNombre;
            producto.ProductoDescripcion = productoDto.ProductoDescripcion;
            producto.ProductoPrecio = productoDto.ProductoPrecio;




            if (!_productoRepository.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return Ok(2);
            }
            return Ok(1);
        }

        [HttpPut("BorrarProducto")]
        public IActionResult BorrarProducto(ProductoActualizarDto productoId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 11);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Eliminar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var producto = _productoRepository.GetProducto(productoId.ProductoId);
            if(producto == null || producto.ProductoEstado == false)
            {
                Ok("El proveedor no existe");
            }

            producto.ProductoEstado = false;

            if (!_productoRepository.BorrarProducto(producto))
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
