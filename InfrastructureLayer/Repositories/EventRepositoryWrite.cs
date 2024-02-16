using DomainLayer.Models;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class EventRepositoryWrite : IEventRepositoryWrite
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<Event> _event;
        public EventRepositoryWrite(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _event = _applicationDbContext.Set<Event>();
        }

        public async Task DeleteAsync(Event eventItem)
        {
            _event.Remove(eventItem);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(Event eventItem)
        {
            await _event.AddAsync(eventItem);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event eventItem)
    {
            _event.Update(eventItem);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
