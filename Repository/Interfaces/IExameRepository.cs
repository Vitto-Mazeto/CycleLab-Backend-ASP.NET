using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IExameRepository : IBaseRepository
    {

        /// <summary>
        /// Obtém todos os exames.
        /// </summary>
        /// <returns>Uma coleção de exames.</returns>
        Task<IEnumerable<Exame>> GetExamesAsync();

        /// <summary>
        /// Obtém um exame pelo seu ID.
        /// </summary>
        /// <returns>O exame encontrado.</returns>
        Task<Exame> GetExameByIdAsync(int id);
    }
}
