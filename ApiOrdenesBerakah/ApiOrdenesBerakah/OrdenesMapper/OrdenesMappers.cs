using ApiOrdenesBerakah.Modelos;
using ApiOrdenesBerakah.Modelos.Dtos;
using AutoMapper;

namespace ApiOrdenesBerakah.OrdenesMapper
{
    public class OrdenesMappers : Profile
    {
        public OrdenesMappers()
        {
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
