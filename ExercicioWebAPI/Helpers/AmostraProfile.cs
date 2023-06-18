using AutoMapper;
using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using ExercicioWebAPI.Models.Entities;

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
