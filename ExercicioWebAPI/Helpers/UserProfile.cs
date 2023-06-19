using AutoMapper;
using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ExercicioWebAPI.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterViewModel, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));

            CreateMap<IdentityResult, UserRegisterResponse>()
                .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(src => src.Succeeded));

            CreateMap<SignInResult, UserLoginResponse>()
                .ForMember(dest => dest.Sucesso, opt => opt.MapFrom(src => src.Succeeded));
        }
    }
}
