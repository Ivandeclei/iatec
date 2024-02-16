using DomainLayer.Models;
using DomainLayer.Models.@enum;
using DomainServiceLayer.CommonConstants;
using DomainServiceLayer.CustomExceptionService;
using DomainServiceLayer.Interfaces;
using InfrastructureLayer.Repositories.Interfaces;

namespace DomainServiceLayer
{
    public class ParticipantEventService : IParticipantEventService
    {
        private readonly IParticipantEventRepositoryWrite _iParticipantEventRepositoryWrite;
        private readonly IEventRepositoryRead _eventRepositoryRead;
        public ParticipantEventService(IParticipantEventRepositoryWrite iParticipantEventRepositoryWrite, IEventRepositoryRead eventRepositoryRead)
        {
            _iParticipantEventRepositoryWrite = iParticipantEventRepositoryWrite;
            _eventRepositoryRead = eventRepositoryRead;

        }
        public async Task DeleteAsync(EventParticipant eventParticipant)
        {
            ValidateEventParticipant(eventParticipant);
            await _iParticipantEventRepositoryWrite.DeleteAsync(eventParticipant);
        }

        public async Task InsertAsync(EventParticipant eventParticipant)
        {
            ValidateEventParticipant(eventParticipant);
            var eventItem = await _eventRepositoryRead.GetAsync(eventParticipant.EventsId);
            ValidateEventBeforeInsert(eventItem);
            await _iParticipantEventRepositoryWrite.InsertAsync(eventParticipant);
        }
        private void ValidateEventParticipant(EventParticipant eventParticipant)
        {
            if (eventParticipant == null)
            {
                throw new ArgumentNullException(nameof(EventParticipant));
            }
        }
        private void ValidateEventBeforeInsert(Event eventItem){
            if (eventItem.Status != StatusEnum.Active )
            {
                throw new CustomException(ExceptionMessages.EVENT_IS_NOT_ACTIVE);
            }

            if (eventItem.TypeEvent != EventTypeEnum.Shared)
            {
                throw new CustomException(ExceptionMessages.EVENT_NOT_SHARED);
            }
        }
    }
}
