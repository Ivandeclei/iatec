using DomainLayer.Models;

namespace InfrastructureLayer.Repositories.Interfaces
{
    public interface IUserRepositoryRead
    {
        /// <summary>
        /// Fetches the data according to the identifier
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// Returns user record found
        /// </returns>
        Task<User> FindByLogin(User user);
    }
}
