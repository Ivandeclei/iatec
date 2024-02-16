using DomainLayer.Models;

namespace InfrastructureLayer.Repositories.Interfaces
{
    public interface IRepositoryBaseRead<T> where T : class
    {
        /// <summary>
        /// Search for data by your identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Verifies that a piece of data exists according to the record identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Boolean> AnyDataAsync(Guid id);
    }
}
