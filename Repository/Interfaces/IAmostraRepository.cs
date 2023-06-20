using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IAmostraRepository : IBaseRepository
    {
        Task<IEnumerable<Amostra>> GetAmostrasAsync();
        Task<Amostra> GetAmostraByIdAsync(int id);
    }
}
