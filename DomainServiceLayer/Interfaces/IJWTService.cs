using DomainLayer.Models;

namespace DomainServiceLayer.Interfaces
{
    public interface IJWTService
    {
        /// <summary>
        /// Method responsible for generating a JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return as Token</returns>
        string CreateToken(User user);

    }
}
