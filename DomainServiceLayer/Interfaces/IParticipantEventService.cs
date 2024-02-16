using DomainLayer.Models;

namespace DomainServiceLayer.Interfaces
{
    public interface IParticipantEventService
    {
        /// <summary>
        /// Method Responsible for Deleting Registry
        /// </summary>
        /// <param name="eventParticipant"></param>
        /// <returns></returns>
        Task DeleteAsync(EventParticipant eventParticipant);

        /// <summary>
        /// Method Responsible for Add Record
        /// </summary>
        /// <param name="eventParticipant"></param>
        /// <returns></returns>
        Task InsertAsync(EventParticipant eventParticipant);
    }
}
