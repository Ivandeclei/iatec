using DomainLayer.Models;
using InfrastructureLayer.Context;
using InfrastructureLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class UserRepositoryRead : IUserRepositoryRead
    {

        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<User> _user;
        
        public UserRepositoryRead(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _user = _applicationDbContext.Set<User>();
        }
        public async Task<User> FindByLogin(User user)
        {
            var query = _user.AsQueryable();

                query = query.Where(e => e.Email == user.Email);
                return await query.SingleOrDefaultAsync();        }
    }
}
