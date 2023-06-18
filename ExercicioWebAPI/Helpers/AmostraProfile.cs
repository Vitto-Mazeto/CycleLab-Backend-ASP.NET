using AutoMapper;
using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Models.ViewModels;

namespace ExercicioWebAPI.Helpers
{
    public class AmostraProfile : Profile
    {
        public AmostraProfile()
        {
            CreateMap<Amostra, AmostraDto>();
            CreateMap<AmostraAddViewModel, Amostra>();
            CreateMap<AmostraUpdateViewModel, Amostra>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
