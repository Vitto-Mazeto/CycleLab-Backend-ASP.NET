using Database.Context;
using Repository.Interfaces;

namespace Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ExcWebAPIContext _context;

        public BaseRepository(ExcWebAPIContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.ChangeTracker.Clear(); //TODO: entender exatamente como resolver esse problema
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
