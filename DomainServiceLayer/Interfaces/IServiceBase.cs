namespace DomainServiceLayer.Interfaces
{
    public interface IServiceBase<T> where T : class
    {
        /// <summary>
        /// Method Responsible for Deleting Registry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Method Responsible for Add Record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Method Responsible for Updating Registry
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Method Responsible for Fetching Record by Its Identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);
    }
}
