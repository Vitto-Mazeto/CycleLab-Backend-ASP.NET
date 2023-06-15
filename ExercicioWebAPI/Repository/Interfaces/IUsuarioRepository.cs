using ExercicioWebAPI.Models.Entities;

namespace ExercicioWebAPI.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuarioById(int id);
    }
}
