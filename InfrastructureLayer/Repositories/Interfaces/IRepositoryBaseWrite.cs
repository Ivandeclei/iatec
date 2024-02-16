using DomainLayer.Models;

namespace InfrastructureLayer.Repositories.Interfaces
{
    public interface IRepositoryBaseWrite<T> where T : class
    {
        /// <summary>
        /// Deletes a record from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(T id);

        /// <summary>
        /// Inserts a record from the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Updates a database record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
    }
}
