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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ClienteController(IUsuarioRepository usuarioRepository, IClienteRepository clienteRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearCliente(ClienteDto clienteDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 7);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Agregar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (clienteDto == null)
            {
                return Ok("Todos los datos son requeridos");
            }

            if (_clienteRepository.ExisteClienteName(clienteDto.ClienteNombre))
            {
                return Ok("Ya existe un cliente con este nombre ");
            }
            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.ClienteFechaCreacion = DateTime.Now;

            if (!_clienteRepository.CrearCliente(cliente))
            {
                return Ok("Algo salio mal guardando el registro{cliente.ClienteNombre}");
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetClientes()
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 7);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var listaclientes = _clienteRepository.GetClientes();
            var listaClientesDto = new List<ClienteDto>();

            foreach (var lista in listaclientes)
            {
                listaClientesDto.Add(_mapper.Map<ClienteDto>(lista));
            }
            return Ok(listaClientesDto);
        }

        [HttpGet("{clienteId:int}", Name = "GetCliente")]
        public IActionResult GetCliente(int clienteId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 7);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Consultar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var itemCliente = _clienteRepository.GetCliente(clienteId);

            if (itemCliente == null)
            {
                return NotFound();
            }

            var itemClienteDto = _mapper.Map<ClienteDto>(itemCliente);
            return Ok(itemClienteDto);
        }


        [HttpPut("{clienteId:int}", Name = "ActualizarCliente")]
        public IActionResult ActualizarCliente(int clienteId, [FromBody]ClienteDto clienteDto)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 7);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Modificar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            if (clienteDto == null || clienteId != clienteDto.ClienteId)
            {
                return Ok("Todos los campos son necesarios");
            }

            var cliente = _clienteRepository.GetCliente(clienteDto.ClienteId);
            if (cliente == null)
            {
                return Ok("El cliente ya no existe");
            }

            cliente.ClienteNombre = clienteDto.ClienteNombre;
            cliente.ClienteApellido = clienteDto.ClienteApellido;
            cliente.ClienteTelefono = clienteDto.ClienteTelefono;
            cliente.ClienteCorreo = clienteDto.ClienteCorreo;
            cliente.ClienteNit = clienteDto.ClienteNit;
            cliente.ClienteDireccion = clienteDto.ClienteDireccion;


            if (!_clienteRepository.ActualizarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{cliente.ClienteNombre}");
                return Ok(ModelState);
            }
            return Ok(1);
        }

        [HttpPut("BorrarCliente")]
        public IActionResult BorrarCliente(ClienteActualizarDto clienteId)
        {
            var permiso = _usuarioRepository.GetUsuarioPermisos(UsuarioAutenticado(), 7);
            if (permiso == null)
            {
                return Ok("Error al realizar la accion, contact con su superior");
            }

            if (permiso.Eliminar == false)
            {
                return Ok("No puedes hacer esta accion");
            }

            var cliente = _clienteRepository.GetCliente(clienteId.ClienteId);

            if(cliente == null || cliente.ClienteEstado == false)
            {
                return Ok("El cliene no existe");
            }

            cliente.ClienteEstado = false;

            if (!_clienteRepository.ActualizarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{cliente.ClienteNombre}");
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
