using Database.Context;
using Domain.Entities;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
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
            return await _context.Amostras.ToListAsync();
        }

        public async Task<Amostra> GetAmostraByIdAsync(int id)
        {
            return await _context.Amostras.Include(a => a.Exames)
                                          .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
