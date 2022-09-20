﻿using AutoMapper;
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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ClienteController(IClienteRepository clienteRepository, IMapper mapper, IConfiguration config)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearCliente(ClienteDto clienteDto)
        {
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
            if (clienteDto == null || clienteId != clienteDto.ClienteId)
            {
                return BadRequest(ModelState);
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

        [HttpDelete("{clienteId:int}", Name = "BorrarCliente")]
        public IActionResult BorrarCliente(int clienteId)
        {

            if (!_clienteRepository.ExisteCliente(clienteId))
            {
                return NotFound();
            }

            var cliente = _clienteRepository.GetCliente(clienteId);

            if (!_clienteRepository.BorrarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{cliente.ClienteNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
