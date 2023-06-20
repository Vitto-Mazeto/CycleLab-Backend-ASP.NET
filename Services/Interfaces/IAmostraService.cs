using DTOs.Responses;
using DTOs.ViewModels;

namespace Services.Interfaces
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
