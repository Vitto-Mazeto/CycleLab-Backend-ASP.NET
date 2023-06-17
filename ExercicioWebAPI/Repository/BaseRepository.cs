using ExercicioWebAPI.Context;
using ExercicioWebAPI.Repository.Interfaces;

namespace ExercicioWebAPI.Repository
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

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
