using AutoFixture;
using DomainLayer.Models;
using DomainServiceLayer;
using DomainServiceLayer.CommonConstants;
using DomainServiceLayer.Interfaces;
using DomainServiceLayer.Utils;
using InfrastructureLayer.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DomainService.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepositoryRead> _userRepositoryReadMock;
        private readonly Mock<IJWTService> _jwtServiceMock;
        private readonly UserService _userService;
        private readonly Fixture _fixture;

        public UserServiceTests()
        {
            _userRepositoryReadMock = new Mock<IUserRepositoryRead>();
            _jwtServiceMock = new Mock<IJWTService>();
            _userService = new UserService(_userRepositoryReadMock.Object, _jwtServiceMock.Object);
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Fact]
        public async Task FindByUser_ReturnsAuthenticatedFalse_WhenUserNotFound()
        {
            // Arrange
            var user = _fixture.Create<User>();
            _userRepositoryReadMock.Setup(x => x.FindByLogin(user)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.FindByUser(user);

            // Assert
            Assert.False((bool)result.GetType().GetProperty("authenticated").GetValue(result, null));
        }


        [Fact]
        public async Task FindByUser_ReturnsAuthenticatedTrue_WhenUserFoundAndPasswordCorrect()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var userResult = _fixture.Create<User>();
            _userRepositoryReadMock.Setup(x => x.FindByLogin(user)).ReturnsAsync(userResult);
            userResult.Password = HashPassword.EncriptPassword(user);
            _jwtServiceMock.Setup(x => x.CreateToken(user)).Returns("token");

            // Act
            var result = await _userService.FindByUser(user);

            // Assert
            Assert.True((bool)result.GetType().GetProperty("authenticated").GetValue(result, null));
        }

        [Fact]
        public async Task FindByUser_ReturnsCorrectToken_WhenUserFoundAndPasswordCorrect()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var userResult = _fixture.Create<User>();
            _userRepositoryReadMock.Setup(x => x.FindByLogin(user)).ReturnsAsync(userResult);
            userResult.Password = HashPassword.EncriptPassword(user);
            var expectedToken = "token";
            _jwtServiceMock.Setup(x => x.CreateToken(user)).Returns(expectedToken);

            // Act
            var result = await _userService.FindByUser(user);

            // Assert
            Assert.Equal(expectedToken, result.GetType().GetProperty("token").GetValue(result, null));
        }

        [Fact]
        public async Task FindByUser_ReturnsCorrectMessage_WhenUserNotFound()
        {
            // Arrange
            var user = _fixture.Create<User>();
            _userRepositoryReadMock.Setup(x => x.FindByLogin(user)).ReturnsAsync((User)null);
            var expectedMessage = ExceptionMessages.IS_AUTHENTICATED_FALSE;

            // Act
            var result = await _userService.FindByUser(user);

            // Assert
            Assert.Equal(expectedMessage, result.GetType().GetProperty("message").GetValue(result, null));
        }

        [Fact]
        public async Task FindByUser_ReturnsCorrectMessage_WhenUserFoundAndPasswordCorrect()
        {
            // Arrange
            var user = _fixture.Create<User>();
            var userResult = _fixture.Create<User>();
            _userRepositoryReadMock.Setup(x => x.FindByLogin(user)).ReturnsAsync(userResult);
            userResult.Password = HashPassword.EncriptPassword(user);
            _jwtServiceMock.Setup(x => x.CreateToken(user)).Returns("token");
            var expectedMessage = ExceptionMessages.IS_AUTHENTICATED_TRUE;

            // Act
            var result = await _userService.FindByUser(user);

            // Assert
            Assert.Equal(expectedMessage, result.GetType().GetProperty("message").GetValue(result, null));
        }

        [Fact]
        public async Task FindByUser_ReturnsAuthenticatedFalse_WhenPasswordIncorrect()
        {
            // Arrange
            var user = _fixture.Build<User>().With(u => u.Password, "correctPassword").Create();
            _userRepositoryReadMock.Setup(repo => repo.FindByLogin(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.FindByUser(_fixture.Build<User>().With(u => u.Password, "incorrectPassword").Create());

            // Assert
            Assert.False((bool)result.GetType().GetProperty("authenticated").GetValue(result, null));
        }

        [Fact]
        public async Task FindByUser_ReturnsCorrectMessage_WhenPasswordIncorrect()
        {
            // Arrange
            var user = _fixture.Build<User>().With(u => u.Password, "correctPassword").Create();
            _userRepositoryReadMock.Setup(repo => repo.FindByLogin(user)).ReturnsAsync(user);

            // Act
            var result = await _userService.FindByUser(_fixture.Build<User>().With(u => u.Password, "incorrectPassword").Create());

            // Assert
            Assert.Equal(ExceptionMessages.IS_AUTHENTICATED_FALSE, result.GetType().GetProperty("message").GetValue(result, null));
        }


    }

}
