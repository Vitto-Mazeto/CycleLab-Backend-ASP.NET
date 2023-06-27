using AutoMapper;
using DTOs.Responses;
using DTOs.ViewModels;
using Domain.Entities;

namespace DTOs.Helpers
{
    public class AmostraProfile : Profile
    {
        public AmostraProfile()
        {
            CreateMap<Amostra, AmostraDto>();
            CreateMap<AmostraDto, Amostra>();
            CreateMap<AmostraAddViewModel, Amostra>();
            CreateMap<AmostraUpdateViewModel, Amostra>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
