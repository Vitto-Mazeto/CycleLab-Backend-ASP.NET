using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IAmostraRepository : IBaseRepository
    {

        /// <summary>
        /// Obtém todas as amostras.
        /// </summary>
        /// <returns>Uma coleção de amostras.</returns>
        Task<IEnumerable<Amostra>> GetAmostrasAsync();

        /// <summary>
        /// Obtém uma amostra pelo seu ID.
        /// </summary>
        /// <returns>A amostra encontrada.</returns>
        Task<Amostra> GetAmostraByIdAsync(int id);
    }
}
