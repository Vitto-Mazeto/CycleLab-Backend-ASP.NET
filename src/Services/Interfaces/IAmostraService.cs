using Domain.Entities;
using DTOs.Responses;
using DTOs.ViewModels;

namespace Services.Interfaces
{
    public interface IAmostraService
    {

        /// <summary>
        /// Obtém todas as amostras assincronamente.
        /// </summary>
        /// <returns>A lista de amostras como objetos AmostraDto.</returns>
        Task<IEnumerable<AmostraDto>> GetAmostrasAsync();

        /// <summary>
        /// Obtém uma amostra por ID assincronamente.
        /// </summary>
        /// <returns>A amostra encontrada como objeto AmostraDto.</returns>
        Task<AmostraDto> GetAmostraByIdAsync(int id);

        /// <summary>
        /// Adiciona uma nova amostra assincronamente.
        /// </summary>
        /// <returns>A tarefa completada após a adição da amostra.</returns>
        Task AddAmostraAsync(Amostra amostra);

        /// <summary>
        /// Atualiza uma amostra existente assincronamente.
        /// </summary>
        /// <returns>A tarefa completada após a atualização da amostra.</returns>
        Task UpdateAmostraAsync(Amostra amostra);

        /// <summary>
        /// Exclui uma amostra existente assincronamente.
        /// </summary>
        /// <returns>A tarefa completada após a exclusão da amostra.</returns>
        Task DeleteAmostraAsync(int id);
    }
}
