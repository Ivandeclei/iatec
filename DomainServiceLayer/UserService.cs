using DomainLayer.Models;
using DomainServiceLayer.CommonConstants;
using DomainServiceLayer.Interfaces;
using DomainServiceLayer.Utils;
using InfrastructureLayer.Repositories.Interfaces;

namespace DomainServiceLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoryRead _userRepositoryRead;
        private readonly IJWTService _jwtService;
        public UserService(IUserRepositoryRead userRepositoryRead, IJWTService jwtService)
        {
            _userRepositoryRead = userRepositoryRead;
            _jwtService = jwtService;
        }
        public async Task<object> FindByUser(User user)
        {
            
            var userResult = await _userRepositoryRead.FindByLogin(user);

            if (userResult == null)
            {
                return new
                {
                    authenticated = false,
                    message = ExceptionMessages.IS_AUTHENTICATED_FALSE,
                };
            }

            if (!HashPassword.VerifyPassword(userResult, user.Password))
            {
                return new
                {
                    authenticated = false,
                    message = ExceptionMessages.IS_AUTHENTICATED_FALSE,
                };
            }
            else
            {
                var token = _jwtService.CreateToken(user);
                return new
                {
                    authenticated = true,
                    token = $"Bearer {token}",
                    message = ExceptionMessages.IS_AUTHENTICATED_TRUE,
                };
            }
        }
    }
}
