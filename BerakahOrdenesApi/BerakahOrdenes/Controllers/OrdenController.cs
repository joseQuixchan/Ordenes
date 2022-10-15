using AutoMapper;
using BerakahOrdenes.Modelos;

using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rotativa.AspNetCore;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wkhtmltopdf.NetCore;

namespace BerakahOrdenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IOrdenDetalleRepository _ordenDetalleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IGeneratePdf _pdf;

        public OrdenController(IGeneratePdf pdf, IOrdenRepository ordenRepository, IOrdenDetalleRepository ordenDetalleRepository, IMapper mapper, IConfiguration config)
        {
            _pdf = pdf;
            _ordenRepository = ordenRepository;
            _ordenDetalleRepository = ordenDetalleRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearOrden(OrdenDto ordenDto)
        {
            if (ordenDto == null)
            {
                return BadRequest(ModelState);
            }

            var orden = new Orden();
            orden.ClienteNombre = ordenDto.ClienteNombre;
            orden.UsuarioNombre = ordenDto.UsuarioNombre;
            if (ordenDto.ClienteNit == "")
            {
                orden.ClienteNit = "No especificado";
            }
            orden.ClienteNit = ordenDto.ClienteNit;
            if(ordenDto.ClienteDireccion == "")
            {
                orden.ClienteDireccion = "No especificado";
            }
            orden.ClienteDireccion = ordenDto.ClienteDireccion;
            if (orden.ClienteCorreo == "")
            {
                orden.ClienteCorreo = "No especificado";
            }
            orden.ClienteCorreo = ordenDto.ClienteCorreo;
            if (orden.ClienteTelefono == "")
            {
                orden.ClienteTelefono = "No especificado";
            }
            orden.ClienteTelefono = ordenDto.ClienteTelefono;
            orden.OrdenEstado = ordenDto.OrdenEstado;
            orden.OrdenFechaCreacion = DateTime.Now;
            orden.OrdenFechaEntrega = ordenDto.OrdenFechaEntrega;
            

            if (!_ordenRepository.CrearOrden(orden))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro de la orden");
                return Ok(2);
            }

            foreach(var detalle in ordenDto.OrdenDetalles)
            {
                OrdenDetalle detalles = new OrdenDetalle();
                detalles.OrdenId = orden.OrdenId;
                detalles.Cantidad = detalle.Cantidad;
                detalles.NombreProducto = detalle.NombreProducto;
                detalles.PrecioUniario = detalle.PrecioUniario;
                detalles.Total = detalle.Cantidad * detalle.PrecioUniario;
                detalles.Descripcion = detalle.Descripcion;
                detalles.OrdenDetalleEstado = true;
                detalles.OrdenDetalleFechaCreacion = DateTime.Now;

                if (!_ordenDetalleRepository.CrearOrdenDetalle(detalles))
                {
                    ModelState.AddModelError("", $"Algo Salio Mal guardando el registro de la orden");
                    return Ok(0);
                }
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetOrden()
        {
            var listaOrdenes = _ordenRepository.GetOrdenes();
            var listaOrdenesDto = new List<OrdenDto>();



            foreach (var lista in listaOrdenes)
            {
                
                listaOrdenesDto.Add(_mapper.Map<OrdenDto>(lista));
            }

            return Ok(listaOrdenesDto);
        }

        [HttpGet("GenerarPdf")]
        public async Task<IActionResult> GetOrdenPDF(int ordenId)
        {
            if(ordenId < 0)
            {
                return Ok("El numero de orden es necesario");
            }
            var orden = _ordenRepository.GetOrden(ordenId);
            decimal subTotal = 0;

            foreach(var total in orden.OrdenDetalles)
            {
                subTotal = total.Total + subTotal;
            }

            var itnemOrden = _mapper.Map<OrdenDto>(orden);
            itnemOrden.Subtotal = subTotal;

            return await _pdf.GetPdf("vistas/ImprimirVenta.cshtml", itnemOrden);
           
        }

        [HttpGet("OrdenDetalles")]
        public ActionResult GetOrdenDetalle(int ordenId)
        {
            if (ordenId < 0)
            {
                return Ok("El numero de orden es necesario");
            }

            var Detalles = _ordenDetalleRepository.GetOrdenDetalles(ordenId);
            var listaDetallesDto = new List<OrdenDetalleDto>();

            foreach (var lista in Detalles)
            {
                listaDetallesDto.Add(_mapper.Map<OrdenDetalleDto>(lista));
            }

            return Ok(listaDetallesDto);

        }

        [HttpGet("{ordenId:int}", Name = "GetOrden")]
        public IActionResult GetOrden(int ordenId)
        {
            var itemOrden = _ordenRepository.GetOrden(ordenId);

            if (itemOrden == null)
            {
                return NotFound();
            }

            var itemOrdenDto = _mapper.Map<OrdenDto>(itemOrden);
            return Ok(itemOrdenDto);
        }

        [HttpPatch("{ordenId:int}", Name = "ActualizarOrden")]
        public IActionResult ActualizarOrden(int ordenId, [FromBody] OrdenDto ordenDto)
        {
            if (ordenDto == null || ordenId != ordenDto.OrdenId)
            {
                return BadRequest(ModelState);
            }

            var orden = _mapper.Map<Orden>(ordenDto);

            if (!_ordenRepository.ActualizarOrden(orden))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ordenId:int}", Name = "BorrarOrden")]
        public IActionResult BorrarOrden(int ordenId)
        {

            if (!_ordenRepository.ExisteOrden(ordenId))
            {
                return NotFound();
            }

            var orden = _ordenRepository.GetOrden(ordenId);

            if (!_ordenRepository.BorrarOrden(orden))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
