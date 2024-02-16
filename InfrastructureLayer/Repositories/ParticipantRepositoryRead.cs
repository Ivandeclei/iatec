using DomainLayer.Models;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class ParticipantRepositoryRead : IParticipantRepositoryRead
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<Participant> _participant;
        public ParticipantRepositoryRead(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _participant = _applicationDbContext.Set<Participant>();
        }

        public async Task<bool> AnyDataAsync(Guid id)
        {
            return await _participant.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Participant>> GetAllAsync(PaginateFilter? paginateFilter)
        {
            return await _participant.Include(s => s.Events)
                .ThenInclude(ep => ep.Event)
                                .Skip((((int)paginateFilter.PageNumber - 1) * (int)paginateFilter.PageSize))
                                .Take((int)paginateFilter.PageSize)
                                .ToListAsync(); 
        }

        public async Task<Participant> GetAsync(Guid id)
        {
            return await _participant.Include(s => s.Events)
                                .ThenInclude(ep => ep.Event)
                               .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
