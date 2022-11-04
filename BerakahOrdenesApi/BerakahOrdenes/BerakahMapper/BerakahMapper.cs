using AutoMapper;
using BerakahOrdenes.Modelos;
using BerakahOrdenes.Modelos.Dtos;

namespace BerakahOrdenes.BerakahMapper
{
    public class BerakahMapper : Profile
    {
        public BerakahMapper()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioActualizarDto>().ReverseMap();
            CreateMap<Usuario, UsuarioPerfilDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Modulo, ModuloDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<UsuarioRol, UsuarioRolDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<RolMenu, RolMenuDto>().ReverseMap();
            CreateMap<Proveedor, ProveedorDto>().ReverseMap();
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Orden, OrdenDto>().ReverseMap();
            CreateMap<Orden, OrdenViewDto>().ReverseMap();
            CreateMap<OrdenDetalle, OrdenDetalleDto>().ReverseMap();
            CreateMap<Tarea, TareaDto>().ReverseMap();
        }
    }
}
