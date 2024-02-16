using DomainLayer.Models;

namespace InfrastructureLayer.Repositories.Interfaces
{
    public interface IParticipantRepositoryRead : IRepositoryBaseRead<Participant>
    {
        /// <summary>
        /// Fetches all records
        /// </summary>
        /// <param name="paginateFilter"></param>
        /// <returns>
        /// Returns a list of users
        /// </returns>
        Task<IEnumerable<Participant>> GetAllAsync(PaginateFilter paginateFilter);
    }
}
