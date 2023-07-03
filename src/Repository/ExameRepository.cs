using Database.Context;
using Domain.Entities;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class ExameRepository : BaseRepository, IExameRepository
    {
        private readonly ExcWebAPIContext _context;

        public ExameRepository(ExcWebAPIContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exame>> GetExamesAsync()
        {
            return await _context.Exames.ToListAsync();
        }

        public async Task<Exame> GetExameByIdAsync(int id)
        {
            return await _context.Exames.FindAsync(id);
        }
    }
}
