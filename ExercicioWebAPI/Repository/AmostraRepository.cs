using ExercicioWebAPI.Context;
using ExercicioWebAPI.Models.Entities;
using ExercicioWebAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExercicioWebAPI.Repository
{
    public class AmostraRepository : BaseRepository, IAmostraRepository
    {
        private readonly ExcWebAPIContext _context;
        public AmostraRepository(ExcWebAPIContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Amostra>> GetAmostrasAsync()
        {
            return await _context.Amostras
                         .ToListAsync();
        }

        public async Task<Amostra> GetAmostraByIdAsync(int id)
        {
            return await _context.Amostras.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

    }
}
