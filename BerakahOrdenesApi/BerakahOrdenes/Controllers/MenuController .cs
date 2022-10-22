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
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public MenuController(IMenuRepository menuRepository, IMapper mapper, IConfiguration config)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
            _config = config;
        }


        [HttpPost]
        public IActionResult CrearMenu(MenuDto menuDto)
        {
            if (menuDto == null)
            {
                return BadRequest(ModelState);
            }

            var menu = _mapper.Map<Menu>(menuDto);
            menu.MenuFechaCreacion = DateTime.Now;

            if (!_menuRepository.CrearMenu(menu))
            {
                ModelState.AddModelError("", $"Algo Salio Mal guardando el registro{menu.MenuNombre}");
                return StatusCode(500, ModelState);
            }

            return Ok(1);
        }

        [HttpGet]
        public ActionResult GetMenus()
        {
            var listaMenus = _menuRepository.GetMenus();
            var listaMenusDto = new List<MenuDto>();

            foreach (var lista in listaMenus)
            {
                listaMenusDto.Add(_mapper.Map<MenuDto>(lista));
            }

            return Ok(listaMenusDto);
        }



        [HttpGet("{menuId:int}", Name = "GetMenu")]
        public IActionResult GetMenu(int menuId)
        {
            var itemMenu = _menuRepository.GetMenu(menuId);

            if (itemMenu == null)
            {
                return NotFound();
            }

            var itemMenuDto = _mapper.Map<MenuDto>(itemMenu);
            return Ok(itemMenuDto);
        }

        [HttpPatch("{menuId:int}", Name = "ActualizarMenu")]
        public IActionResult ActualizarMenu(int menuId, [FromBody]MenuDto menuDto)
        {
            if (menuDto == null || menuId != menuDto.MenuId)
            {
                return BadRequest(ModelState);
            }

            var menu = _mapper.Map<Menu>(menuDto);

            if (!_menuRepository.ActualizarMenu(menu))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{menu.MenuNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{menuId:int}", Name = "BorrarMenu")]
        public IActionResult BorrarMenu(int menuId)
        {

            if (!_menuRepository.ExisteMenu(menuId))
            {
                return NotFound();
            }

            var menu = _menuRepository.GetMenu(menuId);

            if (!_menuRepository.BorrarMenu(menu))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{menu.MenuNombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
