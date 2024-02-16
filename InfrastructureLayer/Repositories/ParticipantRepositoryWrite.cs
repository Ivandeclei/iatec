using DomainLayer.Models;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class ParticipantRepositoryWrite : IParticipantRepositoryWrite
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<Participant> _participant;
        public ParticipantRepositoryWrite(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _participant = _applicationDbContext.Set<Participant>();
        }

        public async Task DeleteAsync(Participant participant)
        {
            _participant.Remove(participant);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(Participant participant)
        {
            await _participant.AddAsync(participant);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Participant participant)
        {
            _participant.Update(participant);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
