using AutoFixture;
using DomainLayer.Models;
using DomainServiceLayer;
using DomainServiceLayer.Interfaces;
using InfrastructureLayer.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DomainService.Tests
{

    public class ParticipantServiceTests
    {
        private Mock<IParticipantRepositoryRead> _participantRepositoryRead;
        private Mock<IParticipantRepositoryWrite> _participantRepositoryWrite;
        private IParticipantService _participantService;
        private Fixture _fixture;
        private Participant _participant;

        public ParticipantServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _participantRepositoryRead = new Mock<IParticipantRepositoryRead>();
            _participantRepositoryWrite = new Mock<IParticipantRepositoryWrite>();

            _participantService = new ParticipantService(_participantRepositoryRead.Object, _participantRepositoryWrite.Object);

            _participant = _fixture.Create<Participant>();
        }

        [Fact]
        public async Task GetAsync_WithValidId_ReturnsParticipant()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _participantRepositoryRead.Setup(x => x.GetAsync(id)).ReturnsAsync(_participant);

            // Act
            var result = await _participantService.GetAsync(id);

            // Assert
            Assert.Equal(_participant, result);
            _participantRepositoryRead.Verify(x => x.GetAsync(id), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_WithValidParticipant_InsertsParticipant()
        {
            // Arrange
            _participantRepositoryWrite.Setup(x => x.InsertAsync(_participant)).Returns(Task.CompletedTask);

            // Act
            await _participantService.InsertAsync(_participant);

            // Assert
            _participantRepositoryWrite.Verify(x => x.InsertAsync(_participant), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithValidParticipant_UpdatesParticipant()
        {
            // Arrange
            _participantRepositoryWrite.Setup(x => x.UpdateAsync(_participant)).Returns(Task.CompletedTask);

            // Act
            await _participantService.UpdateAsync(_participant);

            // Assert
            _participantRepositoryWrite.Verify(x => x.UpdateAsync(_participant), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesParticipant()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _participantRepositoryRead.Setup(x => x.GetAsync(id)).ReturnsAsync(_participant);
            _participantRepositoryWrite.Setup(x => x.DeleteAsync(_participant)).Returns(Task.CompletedTask);

            // Act
            await _participantService.DeleteAsync(id);

            // Assert
            _participantRepositoryRead.Verify(x => x.GetAsync(id), Times.Once);
            _participantRepositoryWrite.Verify(x => x.DeleteAsync(_participant), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WithValidPaginateFilter_ReturnsParticipants()
        {
            // Arrange
            var paginateFilter = new PaginateFilter { PageNumber = 1, PageSize = 10 };
            var participants = _fixture.CreateMany<Participant>(10);
            _participantRepositoryRead.Setup(x => x.GetAllAsync(paginateFilter)).ReturnsAsync(participants);

            // Act
            var result = await _participantService.GetAllAsync(paginateFilter);

            // Assert
            Assert.Equal(participants, result);
            _participantRepositoryRead.Verify(x => x.GetAllAsync(paginateFilter), Times.Once);
        }

    }
}

