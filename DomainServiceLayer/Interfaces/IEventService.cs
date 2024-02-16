using DomainLayer.Models;

namespace DomainServiceLayer.Interfaces
{
    public interface IEventService : IServiceBase<Event>
    {
        /// <summary>
        /// Responsible Method for Fetching Records
        /// </summary>
        /// <param name="eventFilterParameters"></param>
        /// <returns></returns>
        Task<IEnumerable<Event>> GetAllAsync(EventFilterParameters eventFilterParameters);
    }
}
