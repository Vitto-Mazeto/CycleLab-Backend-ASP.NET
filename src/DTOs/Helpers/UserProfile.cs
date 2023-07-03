using AutoMapper;
using DTOs.Responses;
using DTOs.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DTOs.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Uso o email tanto para username quanto para o email mesmo
            CreateMap<UserRegisterViewModel, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<IdentityResult, UserRegisterResponse>()
                .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(src => src.Succeeded));

            CreateMap<SignInResult, UserLoginResponse>()
                .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(src => src.Succeeded));
        }
    }
}
