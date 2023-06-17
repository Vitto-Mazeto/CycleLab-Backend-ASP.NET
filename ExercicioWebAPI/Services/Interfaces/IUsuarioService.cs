using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;

namespace ExercicioWebAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetUsuariosAsync();
        Task<UsuarioDto> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(UsuarioAddViewModel usuario);
        Task UpdateUsuarioAsync(int id, UsuarioUpdateViewModel usuario);
        Task DeleteUsuarioAsync(int id);
    }
}
