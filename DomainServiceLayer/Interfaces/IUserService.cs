using DomainLayer.Models;

namespace DomainServiceLayer.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Search user by id
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// Returns the user object
        /// </returns>
        Task<object> FindByUser(User user);
    }
}
