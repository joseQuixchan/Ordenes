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
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Modulo, ModuloDto>().ReverseMap();
            CreateMap<Rol, RolDto>().ReverseMap();
        }
    }
}
