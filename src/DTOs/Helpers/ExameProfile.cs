using AutoMapper;
using DTOs.Responses;
using DTOs.ViewModels;
using Domain.Entities;

namespace DTOs.Helpers
{
    public class ExameProfile : Profile
    {
        public ExameProfile()
        {
            CreateMap<Exame, ExameDto>();
            CreateMap<ExameAddViewModel, Exame>();
            CreateMap<ExameUpdateViewModel, Exame>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
