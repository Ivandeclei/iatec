using DomainLayer.Models;
using DomainServiceLayer.CommonConstants;
using DomainServiceLayer.Interfaces;
using DomainServiceLayer.Utils;
using InfrastructureLayer.Repositories.Interfaces;
using System.Security.Cryptography;

namespace DomainServiceLayer
{
    public class ParticipantService : IParticipantService
    {
        //Generate random numbers
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private readonly IParticipantRepositoryRead _participantRepositoryRead;
        private readonly IParticipantRepositoryWrite _participantRepositoryWrite;
        public ParticipantService(IParticipantRepositoryRead participantRepositoryRead, 
            IParticipantRepositoryWrite participantRepositoryWrite)
        {
            _participantRepositoryRead = participantRepositoryRead;
            _participantRepositoryWrite = participantRepositoryWrite;
        }
        public async  Task DeleteAsync(Guid id)
        {
            var participant = await GetAsync(id);
            ValidateParticipant(participant);

            await _participantRepositoryWrite.DeleteAsync(participant);
        }

        public async Task<IEnumerable<Participant>> GetAllAsync(PaginateFilter paginateFilter)
        {
            if (!paginateFilter.PageNumber.HasValue && !paginateFilter.PageSize.HasValue)
            {
                paginateFilter.PageSize = 10;
                paginateFilter.PageNumber = 1;
            }

            return await _participantRepositoryRead.GetAllAsync(paginateFilter);
        }

        public async Task<Participant> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(ExceptionMessages.ID_IS_EMPTY);
            }

            return await _participantRepositoryRead.GetAsync(id);
        }

        public async Task InsertAsync(Participant participant)
        {
            ValidateParticipant(participant);
            participant.User.Password = HashPassword.EncriptPassword(participant.User);
            await _participantRepositoryWrite.InsertAsync(participant);
        }

        public async Task UpdateAsync(Participant participant)
        {
            ValidateParticipant(participant);
            await _participantRepositoryWrite.UpdateAsync(participant);
        }

        private void ValidateParticipant(Participant participant)
        {
            if (participant == null)
            {
                throw new ArgumentNullException(nameof(Participant));
            }
        }
    }
}
