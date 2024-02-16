using DomainLayer.Models;
using DomainServiceLayer.CommonConstants;
using DomainServiceLayer.CustomExceptionService;
using DomainServiceLayer.Interfaces;
using InfrastructureLayer.Repositories.Interfaces;

namespace DomainServiceLayer
{
    public class EventService : IEventService
    {
        private readonly IEventRepositoryRead _eventRepositoryRead;
        private readonly IEventRepositoryWrite _eventRepositoryWrite;
        private readonly IParticipantRepositoryRead _participantRepositoryRead;
        private readonly IParticipantEventRepositoryWrite _participantEventRepositoryWrite;
        public EventService(IEventRepositoryRead eventRepositoryRead, 
            IEventRepositoryWrite eventRepositoryWrite, 
            IParticipantEventRepositoryWrite participantEventRepositoryWrite, 
            IParticipantRepositoryRead participantRepositoryRead)
        {
            _eventRepositoryRead = eventRepositoryRead;
            _eventRepositoryWrite = eventRepositoryWrite;
            _participantEventRepositoryWrite = participantEventRepositoryWrite;
            _participantRepositoryRead = participantRepositoryRead;

        }

        public async Task DeleteAsync(Guid id)
        {
            var eventItem = await GetAsync(id);
            ValidateEvent(eventItem);

            await _eventRepositoryWrite.DeleteAsync(eventItem);
        }

        public async Task<IEnumerable<Event>> GetAllAsync(EventFilterParameters paginateFilter)
        {
            if (!paginateFilter.PageNumber.HasValue && !paginateFilter.PageSize.HasValue)
            {
                paginateFilter.PageSize = 10;
                paginateFilter.PageNumber = 1;
            }

            return await _eventRepositoryRead.GetAllAsync(paginateFilter);
        }

        public async Task<Event> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(ExceptionMessages.ID_IS_EMPTY);
            }

            return await _eventRepositoryRead.GetAsync(id);
        }

        public async Task InsertAsync(Event entity)
        {
            ValidateEvent(entity);
            await ValidateParticipantAsync(entity.ParticipantId);

            await _eventRepositoryWrite.InsertAsync(entity);
            await _participantEventRepositoryWrite.InsertAsync(
                new EventParticipant { 
                    EventsId = entity.Id, 
                    ParticipantsId = entity.ParticipantId 
                });
        }

        public async  Task UpdateAsync(Event entity)
        {
            ValidateEvent(entity);
            await _eventRepositoryWrite.UpdateAsync(entity);
        }

        private void ValidateEvent(Event eventItem)
        {
            if (eventItem == null)
            {
                throw new ArgumentNullException(nameof(Event));
            }
        }

        private async Task ValidateParticipantAsync(Guid participantId)
        {
            var result = await _participantRepositoryRead.AnyDataAsync(participantId);
            if (!result)
            {
                throw new CustomException(ExceptionMessages.PARTICIPANT_NOT_FIND);
            }
        }
    }
}
