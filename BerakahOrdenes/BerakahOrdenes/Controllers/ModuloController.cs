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
    public class ModuloController : ControllerBase
    {
        private readonly IModuloRepository _moduloRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ModuloController(IModuloRepository moduloRepository, IMapper mapper, IConfiguration config)
        {
            _moduloRepository = moduloRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearModulo(ModuloDto moduloDto)
        {
            if (moduloDto == null)
            {
                return BadRequest(ModelState);
            }

            var modulo = _mapper.Map<Modulo>(moduloDto);
            modulo.ModuloFechaCreacion = DateTime.Now;

            if (!_moduloRepository.CrearModulo(modulo))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro{modulo.ModuloNombre}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }


        [HttpGet]
        public ActionResult GetModulos()
        {
            var listaModulos = _moduloRepository.GetModulos();
            var listaModulosDto = new List<ModuloDto>();

            foreach (var lista in listaModulos)
            {
                listaModulosDto.Add(_mapper.Map<ModuloDto>(lista));
            }
            return Ok(listaModulosDto);
        }

        [HttpGet("{moduloId:int}", Name = "GetModulo")]
        public IActionResult GetModulo(int moduloId)
        {
            var itemModulo = _moduloRepository.GetModulo(moduloId);

            if (itemModulo == null)
            {
                return NotFound();
            }

            var itemModuloDto = _mapper.Map<ModuloDto>(itemModulo);
            return Ok(itemModuloDto);
        }

        [HttpPatch("{moduloId:int}", Name = "ActualizarModulo")]
        public IActionResult ActualizarModulo(int moduloId, [FromBody]ModuloDto moduloDto)
        {
            if (moduloDto == null || moduloId != moduloDto.ModuloId)
            {
                return BadRequest(ModelState);
            }

            var modulo = _mapper.Map<Modulo>(moduloDto);

            if (!_moduloRepository.ActualizarModulo(modulo))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{modulo.ModuloNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{moduloId:int}", Name = "BorrarModulo")]
        public IActionResult BorrarModulo(int moduloId)
        {

            if (!_moduloRepository.ExisteModulo(moduloId))
            {
                return NotFound();
            }

            var modulo = _moduloRepository.GetModulo(moduloId);

            if (!_moduloRepository.BorrarModulo(modulo))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{modulo.ModuloNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
