using AutoMapper;
using CrudUserDapper.DTO;
using CrudUserDapper.Model;

namespace CrudUserDapper.Mapping
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, ListarUsuariosDTO>().ReverseMap();
        }
    }
}
