using ExercicioWebAPI.Context;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Repository.Interfaces;

namespace ExercicioWebAPI.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private readonly ExcWebAPIContext _context;
        public UsuarioRepository(ExcWebAPIContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario GetUsuarioById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
