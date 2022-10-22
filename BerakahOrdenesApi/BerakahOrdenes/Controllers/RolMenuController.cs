using AutoMapper;
using BerakahOrdenes.Datos;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BerakahOrdenes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolMenuController : ControllerBase
    {
        private readonly IRolMenuRepository _rolMenuRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly DBOrdenes _db;

        public RolMenuController(DBOrdenes db, IRolMenuRepository rolMenuRepository, IMapper mapper, IConfiguration config)
        {
            _db = db;
            _rolMenuRepository = rolMenuRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearRolMenu(RolMenuCrearDto rolMenuDto)
        {
            if (rolMenuDto == null)
            {
                return Ok("Es necesario un Rol-Menu");
            }

            var rolmenu = _db.RolMenu.Where(w => w.RolId == rolMenuDto.RolId && w.MenuId == rolMenuDto.MenuId).FirstOrDefault();

            if(rolmenu != null)
            {
                rolmenu.RolMenuEstado = true;
                if (!_rolMenuRepository.ActualizarRolMenu(rolmenu))
                {
                    return Ok(2);
                }

                return Ok(1);
            }

            var roleMenuCrear = new RolMenu();
            roleMenuCrear.RolId = rolMenuDto.RolId;
            roleMenuCrear.MenuId = rolMenuDto.MenuId;
            roleMenuCrear.UsuarioId = UsuarioAutenticado();
            roleMenuCrear.Agregar = false;
            roleMenuCrear.Modificar = false;
            roleMenuCrear.Consultar = false;
            roleMenuCrear.Eliminar = false; 
            roleMenuCrear.Imprimir = false;
            roleMenuCrear.RolMenuEstado = true;
            roleMenuCrear.RolMenuFechaCreacion = DateTime.Now;

            if (!_rolMenuRepository.CrearRolMenu(roleMenuCrear))
            {
                return Ok(2);
            }

            return Ok(1);
        }


        [HttpGet]
        public ActionResult GetRolMenu()
        {
            var listaRolMenus = _rolMenuRepository.GetRolMenus();
            var listaRolesMenusDto = new List<RolMenuDto>();

            foreach (var lista in listaRolMenus)
            {
                listaRolesMenusDto.Add(_mapper.Map<RolMenuDto>(lista));
            }

            return Ok(listaRolesMenusDto);
        }

        [HttpGet("MenuRol")]
        public IActionResult GetRolMenu(int rolMenuId)
        {
            var itemRolMenu = _rolMenuRepository.GetRolMenu(rolMenuId);

            if (itemRolMenu == null)
            {
                return Ok(2);
            }

            var itemRolMenuDto = _mapper.Map<RolMenuDto>(itemRolMenu);
            return Ok(itemRolMenuDto);
        }

        [HttpGet("{rolId:int}", Name = "GetRolMenuRol")]
        public ActionResult GetRolMenuPorRol(int rolId)
        {
            var itemRolMenu = _rolMenuRepository.GetRolMenusPorRol(rolId);
            var listaRolesMenusDto = new List<RolMenuDto>();

            if (itemRolMenu == null)
            {
                return Ok(2);
            }

            foreach (var lista in itemRolMenu)
            {
                listaRolesMenusDto.Add(_mapper.Map<RolMenuDto>(lista));
            }

            //var itemRolMenuDto = _mapper.Map<RolMenuDto>(itemRolMenu);
            return Ok(listaRolesMenusDto);
        }

        [HttpPut]
        public IActionResult ActualizarRolMenu(RolMenuCrearDto rolMenuDto)
        {
            if (rolMenuDto == null)
            {
               return Ok(2);
            }

            var rolmenu = _db.RolMenu.Where(w => w.RolId == rolMenuDto.RolId && w.MenuId == rolMenuDto.MenuId).FirstOrDefault();
            
            if (rolmenu != null)
            {
                rolmenu.RolMenuEstado = false;
                if (!_rolMenuRepository.ActualizarRolMenu(rolmenu))
                {
                    return Ok(2);
                }

                return Ok(1);
            }

            return Ok(2);
        }

        [HttpPut("Permisos")]
        public IActionResult ActualizarRolMenu(RolMenuActualizarDto rolMenuDto)
        {
            if (rolMenuDto == null)
            {
                return Ok(2);
            }

            var itemRolMenu = _rolMenuRepository.GetRolMenu(rolMenuDto.RolMenuId);


            if (itemRolMenu != null)
            {
                itemRolMenu.Agregar = rolMenuDto.Agregar;
                itemRolMenu.Consultar = rolMenuDto.Consultar;
                itemRolMenu.Modificar = rolMenuDto.Modificar;
                itemRolMenu.Eliminar = rolMenuDto.Eliminar;

                if (!_rolMenuRepository.ActualizarRolMenu(itemRolMenu))
                {
                    return Ok(2);
                }

                return Ok(1);
            }

            return Ok(2);
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
