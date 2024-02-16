using DomainLayer.Models;

namespace InfrastructureLayer.Repositories.Interfaces
{
    public interface IEventRepositoryRead : IRepositoryBaseRead<Event>
    {
        /// <summary>
        /// Fetches all records
        /// </summary>
        /// <param name="eventFilterParameters"></param>
        /// <returns>
        /// Return list of events
        /// </returns>
        Task<IEnumerable<Event>> GetAllAsync(EventFilterParameters eventFilterParameters);
    }
}
