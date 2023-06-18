using ExercicioWebAPI.DTOs.Responses;
using ExercicioWebAPI.DTOs.ViewModels;
using ExercicioWebAPI.Models.Entities;

namespace ExercicioWebAPI.Services.Interfaces
{
    public interface IAmostraService
    {
        Task<IEnumerable<AmostraDto>> GetAmostrasAsync();
        Task<AmostraDto> GetAmostraByIdAsync(int id);
        Task AddAmostraAsync(AmostraAddViewModel usuario);
        Task UpdateAmostraAsync(int id, AmostraUpdateViewModel usuario);
        Task DeleteAmostraAsync(int id);
    }
}
