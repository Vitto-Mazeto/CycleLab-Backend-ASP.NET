using Domain.Entities;
using DTOs.Responses;
using DTOs.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IExameService
    {
        /// <summary>
        /// Obtém todos os exames assincronamente.
        /// </summary>
        /// <returns>A lista de exames como objetos ExameDto.</returns>
        Task<IEnumerable<ExameDto>> GetExamesAsync();

        /// <summary>
        /// Obtém um exame por ID assincronamente.
        /// </summary>
        /// <returns>O exame encontrado como objeto ExameDto.</returns>
        Task<ExameDto> GetExameByIdAsync(int id);

        /// <summary>
        /// Adiciona um novo exame assincronamente.
        /// </summary>
        /// <returns>A tarefa completada após a adição do exame.</returns>
        Task AddExameAsync(Exame exame);

        /// <summary>
        /// Atualiza um exame existente assincronamente.
        /// </summary>
        /// <returns>A tarefa completada após a atualização do exame.</returns>
        Task UpdateExameAsync(Exame exame);

        /// <summary>
        /// Exclui um exame existente assincronamente.
        /// </summary>
        /// <returns>A tarefa completada após a exclusão do exame.</returns>
        Task DeleteExameAsync(int id);
    }
}
