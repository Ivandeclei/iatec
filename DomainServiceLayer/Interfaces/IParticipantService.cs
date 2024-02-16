using DomainLayer.Models;

namespace DomainServiceLayer.Interfaces
{
    public interface IParticipantService : IServiceBase<Participant>
    {
        /// <summary>
        /// Responsible Method for Fetching Records
        /// </summary>
        /// <param name="paginateFilter"></param>
        /// <returns>Returns a list</returns>
        Task<IEnumerable<Participant>> GetAllAsync(PaginateFilter paginateFilter);
    }
}
