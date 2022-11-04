using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wkhtmltopdf.NetCore;

namespace BerakahOrdenes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IOrdenDetalleRepository _ordenDetalleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IGeneratePdf _pdf;

        public OrdenController(IUsuarioRepository usuarioRepository, IGeneratePdf pdf, IOrdenRepository ordenRepository, IOrdenDetalleRepository ordenDetalleRepository, IMapper mapper, IConfiguration config)
        {
            _pdf = pdf;
            _usuarioRepository = usuarioRepository;
            _ordenRepository = ordenRepository;
            _ordenDetalleRepository = ordenDetalleRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearOrden(OrdenDto? ordenDto)
        {
            decimal total = 0;
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 2);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Agregar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

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
            orden.Abono = ordenDto.Abono;
            if (orden.Abono == null)
            {
                orden.Abono = 0;
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
                total = total + detalles.Total;
                if (!_ordenDetalleRepository.CrearOrdenDetalle(detalles))
                {
                    ModelState.AddModelError("", $"Algo Salio Mal guardando el registro de la orden");
                    return Ok(0);
                }
            }

            orden.Total = total;
            if (!_ordenRepository.ActualizarOrden(orden))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro de la orden");
                return Ok(2);
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetOrden()
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 2);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }
            var listaOrdenes = _ordenRepository.GetOrdenes();
            var listaOrdenesDto = new List<OrdenDto>();

            foreach (var lista in listaOrdenes)
            {
                
                listaOrdenesDto.Add(_mapper.Map<OrdenDto>(lista));
            }

            return Ok(listaOrdenesDto);
        }

        [HttpGet("OrdenesHoy")]
        public IActionResult GetOrdenesHoy()
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 2);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }
            var listaOrdenes = _ordenRepository.GetOrdenesFehca();
            var listaOrdenesDto = new List<OrdenDto>();

            foreach (var lista in listaOrdenes)
            {

                listaOrdenesDto.Add(_mapper.Map<OrdenDto>(lista));
            }

            return Ok(listaOrdenesDto);
        }

        [HttpGet("Orden")]
        public ActionResult GetOrdenId(int ordenId)
        {
            if (ordenId < 0)
            {
                return Ok("El numero de orden es necesario");
            }
            var orden = _ordenRepository.GetOrden(ordenId);

            if (orden == null)
            {
                return Ok(2);
            }

            decimal subTotal = 0;

            foreach (var total in orden.OrdenDetalles)
            {
                subTotal = total.Total + subTotal;
            }

            var itnemOrden = _mapper.Map<OrdenViewDto>(orden);
            itnemOrden.Subtotal = subTotal;

            return Ok(itnemOrden);
        }


        [AllowAnonymous]
        [HttpGet("GenerarPdf")]
        public async Task<IActionResult> GetOrdenPDF(int ordenId)
        {
           

            if (ordenId < 0)
            {
                return Ok("El numero de orden es necesario");
            }
            var orden = _ordenRepository.GetOrden(ordenId);
            decimal subTotal = 0;

            foreach(var total in orden.OrdenDetalles)
            {
                subTotal = total.Total + subTotal;
            }

            decimal? abono = subTotal - orden.Abono;
            var itnemOrden = _mapper.Map<OrdenDto>(orden);
            itnemOrden.Subtotal = subTotal;
            itnemOrden.Saldo = abono;

            return await _pdf.GetPdf("vistas/ImprimirVenta.cshtml", itnemOrden);
           
        }

        [HttpGet("OrdenDetalles")]
        public ActionResult GetOrdenDetalle(int ordenId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 2);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

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
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 2);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contacta con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var itemOrden = _ordenRepository.GetOrden(ordenId);

            if (itemOrden == null)
            {
                return NotFound();
            }

            var itemOrdenDto = _mapper.Map<OrdenDto>(itemOrden);
            return Ok(itemOrdenDto);
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

        private int UsuarioAutenticado()
        {
            var claims = User.Claims.ToList();
            var usuario = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            int usuarioId = Int32.Parse(usuario);
            return usuarioId;
        }
    }
}
