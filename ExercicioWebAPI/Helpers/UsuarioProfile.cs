using AutoMapper;
using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Models.ViewModels;

namespace ExercicioWebAPI.Helpers
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioAddViewModel, Usuario>();
            CreateMap<UsuarioUpdateViewModel, Usuario>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
