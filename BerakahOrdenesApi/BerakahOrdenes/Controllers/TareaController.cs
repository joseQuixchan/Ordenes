
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
    public class TareaController : ControllerBase
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public TareaController(IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository, IMapper mapper, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _tareaRepository = tareaRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearTarea(TareaCrearDto tareaDto)
        {
            if (tareaDto == null)
            {
                return Ok(2);
            }

            var tarea = new Tarea();
            tarea.TareaFechaCreacion = DateTime.Now;
            tarea.TareaEstado = true;
            tarea.TareaDescripcion = tareaDto.TareaDescripcion;

            if (!_tareaRepository.CrearTarea(tarea))
            {
                return Ok(3);
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetTareass()
        {

            var listaTareas = _tareaRepository.GetTareas();
            var listaTareasDto = new List<TareaDto>();

            foreach (var lista in listaTareas)
            {
                listaTareasDto.Add(_mapper.Map<TareaDto>(lista));
            }
            return Ok(listaTareasDto);
        }

        [HttpPut("{tareaId:int}", Name = "GetTarea")]
        public IActionResult GetCliente(int tareaId)
        {
            var itemTarea = _tareaRepository.GetTarea(tareaId);

            if (itemTarea == null)
            {
                return Ok(2);
            }
            itemTarea.TareaEstado = false;
            _tareaRepository.ActualizarTarea(itemTarea);
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
