namespace Repository.Interfaces
{
    public interface IBaseRepository
    {

        /// <summary>
        /// Adiciona uma entidade ao contexto.
        /// </summary>
        public void Add<T>(T entity) where T : class;

        /// <summary>
        /// Atualiza uma entidade no contexto.
        /// </summary>
        public void Update<T>(T entity) where T : class;

        /// <summary>
        /// Remove uma entidade do contexto.
        /// </summary>
        public void Delete<T>(T entity) where T : class;

        /// <summary>
        /// Salva as alterações no contexto de forma assíncrona.
        /// </summary>
        /// <returns>Um valor booleano indicando se as alterações foram salvas com sucesso.</returns>
        Task<bool> SaveChangesAsync();
    }
}
