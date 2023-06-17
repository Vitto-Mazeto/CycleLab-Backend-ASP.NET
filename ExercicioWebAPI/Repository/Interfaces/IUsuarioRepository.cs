using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;

namespace ExercicioWebAPI.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
    }
}
