using DomainLayer.Models;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class ParticipantEventRepositoryWrite : IParticipantEventRepositoryWrite
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<EventParticipant> _eventParticipant;

        public ParticipantEventRepositoryWrite(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _eventParticipant = _applicationDbContext.Set<EventParticipant>();
        }
        public async Task DeleteAsync(EventParticipant eventParticipant)
        {
            _eventParticipant.Remove(eventParticipant);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(EventParticipant eventParticipant)
        {
            await _eventParticipant.AddAsync(eventParticipant);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
