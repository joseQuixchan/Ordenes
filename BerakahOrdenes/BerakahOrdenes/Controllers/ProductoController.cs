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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ProductoController(IProductoRepository productoRepository, IMapper mapper, IConfiguration config)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearProducto(ProductoDto productoDto)
        {
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
            producto.ProductoDescripcion = productoDto.ProductoNombre;
            



            if (!_productoRepository.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return Ok(ModelState);
            }
            return Ok(1);
        }

        [HttpDelete("{productoId:int}", Name = "BorrarProducto")]
        public IActionResult BorrarProducto(int productoId)
        {

            if (!_productoRepository.ExisteProducto(productoId))
            {
                return NotFound();
            }

            var proveedor = _productoRepository.GetProducto(productoId);

            if (!_productoRepository.BorrarProducto(proveedor))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
