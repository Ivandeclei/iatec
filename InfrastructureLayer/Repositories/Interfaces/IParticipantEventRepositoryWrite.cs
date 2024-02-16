using DomainLayer.Models;

namespace InfrastructureLayer.Repositories.Interfaces
{
    public interface IParticipantEventRepositoryWrite
    {
        /// <summary>
        /// Deletes intermediate table record in a many-to-many relationship
        /// </summary>
        /// <param name="eventParticipant"></param>
        /// <returns></returns>
        Task DeleteAsync(EventParticipant eventParticipant);

        /// <summary>
        /// Adds intermediate table record in a many-to-many relationship
        /// </summary>
        /// <param name="eventParticipant"></param>
        /// <returns></returns>
        Task InsertAsync(EventParticipant eventParticipant);
    }
}
