using ExercicioWebAPI.Context;
using ExercicioWebAPI.Models.DTOs;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExercicioWebAPI.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        private readonly ExcWebAPIContext _context;
        public UsuarioRepository(ExcWebAPIContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios
                         .ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

    }
}
