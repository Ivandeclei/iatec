using DomainLayer.Models;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace InfrastructureLayer.Repositories
{
    public class EventRepositoryRead : IEventRepositoryRead
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<Event> _event;
        public EventRepositoryRead(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _event = _applicationDbContext.Set<Event>();
        }

        public async Task<bool> AnyDataAsync(Guid id)
        {
            return await _event.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync(EventFilterParameters filterParameters)
        {
            var query = _event
                            .Include(s => s.Participants)
                            .ThenInclude(ep => ep.Participant)
                            .AsQueryable(); 

            
            if (filterParameters.DateEvent.HasValue)
            {
                query = query.Where(e => e.DateEvent.Date == filterParameters.DateEvent.Value.Date);
            }
            if (filterParameters.Day.HasValue)
            {
                query = query.Where(e => e.DateEvent.Day == filterParameters.Day.Value);
            }

            if (filterParameters.Month.HasValue)
            {
                var currentMonth = DateTime.Today.Month;
                var currentYear = DateTime.Today.Year;

                query = query.Where(e => e.DateEvent.Month == currentMonth && e.DateEvent.Year == currentYear);
            }

            if (filterParameters.Week.HasValue)
            {
                var firstDayOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday);
                var lastDayOfWeek = firstDayOfWeek.AddDays(6);

                query = query.Where(e => e.DateEvent.Date >= firstDayOfWeek && e.DateEvent.Date <= lastDayOfWeek);
            }
            
            if (!filterParameters.HourEvent.IsNullOrEmpty())
            {
                query = query.Where(e => e.DateEvent.Hour == TimeSpan.Parse(filterParameters.HourEvent).Hours);
            }
            
            if (!string.IsNullOrEmpty(filterParameters.EventName))
            {
                query = query.Where(e => e.Name.Contains(filterParameters.EventName));
            }

            if (!string.IsNullOrEmpty(filterParameters.Description))
            {
                query = query.Where(e => e.Description.Contains(filterParameters.Description));
            }

           
            query = query
                        .Skip((((int)filterParameters.PageNumber - 1) * (int)filterParameters.PageSize))
                        .Take((int)filterParameters.PageSize);

            return await query.ToListAsync();
        }


        public async Task<Event> GetAsync(Guid id)
        {
            return await _event
                .Include(s => s.Participants)
                .ThenInclude(ep => ep.Participant)
                               .SingleOrDefaultAsync(u => u.Id == id);
        }



    }
}
