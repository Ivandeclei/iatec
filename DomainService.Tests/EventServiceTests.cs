using AutoFixture;
using DomainLayer.Models;
using DomainServiceLayer;
using DomainServiceLayer.CustomExceptionService;
using DomainServiceLayer.Interfaces;
using InfrastructureLayer.Repositories;
using InfrastructureLayer.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DomainService.Tests
{
    public class EventServiceTests
    {
        private Mock<IEventRepositoryRead> _eventRepositoryRead;
        private Mock<IEventRepositoryWrite> _eventRepositoryWrite;
        private Mock<IParticipantRepositoryRead> _participantRepositoryRead;
        private Mock<IParticipantEventRepositoryWrite> _participantEventRepositoryWrite;
        private IEventService _eventService;
        private Fixture _fixture;

        public EventServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _eventRepositoryRead = new Mock<IEventRepositoryRead>();
            _eventRepositoryWrite = new Mock<IEventRepositoryWrite>();
            _participantEventRepositoryWrite = new Mock<IParticipantEventRepositoryWrite>();
            _participantRepositoryRead = new Mock<IParticipantRepositoryRead>();

            _eventService = new EventService(
                _eventRepositoryRead.Object, 
                _eventRepositoryWrite.Object, 
                _participantEventRepositoryWrite.Object, 
                _participantRepositoryRead.Object);
        }
        [Fact]
        public async Task DeleteAsync_ValidId_DeletesEvent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var eventItem = new Event { Id = id };
            _eventRepositoryRead.Setup(x => x.GetAsync(id)).ReturnsAsync(eventItem);
            _eventRepositoryWrite.Setup(x => x.DeleteAsync(eventItem)).Returns(Task.CompletedTask);

            // Act
            await _eventService.DeleteAsync(id);

            // Assert
            _eventRepositoryWrite.Verify(x => x.DeleteAsync(eventItem), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ValidParameters_ReturnsEvents()
        {
            // Arrange
            var paginateFilter = new EventFilterParameters { PageNumber = 1, PageSize = 10 };
            var events = new List<Event> { new Event(), new Event() };
            _eventRepositoryRead.Setup(x => x.GetAllAsync(paginateFilter)).ReturnsAsync(events);

            // Act
            var result = await _eventService.GetAllAsync(paginateFilter);

            // Assert
            Assert.Equal(events, result);
        }

        [Fact]
        public async Task GetAsync_ValidId_ReturnsEvent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var eventItem = new Event { Id = id };
            _eventRepositoryRead.Setup(x => x.GetAsync(id)).ReturnsAsync(eventItem);

            // Act
            var result = await _eventService.GetAsync(id);

            // Assert
            Assert.Equal(eventItem, result);
        }

        [Fact]
        public async Task InsertAsync_ValidEvent_InsertsEvent()
        {
            // Arrange
            var eventItem = new Event { Id = Guid.NewGuid(), ParticipantId = Guid.NewGuid() };
            _eventRepositoryWrite.Setup(x => x.InsertAsync(eventItem)).Returns(Task.CompletedTask);
            _participantEventRepositoryWrite.Setup(x => x.InsertAsync(It.IsAny<EventParticipant>())).Returns(Task.CompletedTask);
            _participantRepositoryRead.Setup(x => x.AnyDataAsync(eventItem.ParticipantId)).ReturnsAsync(true);

            // Act
            await _eventService.InsertAsync(eventItem);

            // Assert
            _eventRepositoryWrite.Verify(x => x.InsertAsync(eventItem), Times.Once);
            _participantEventRepositoryWrite.Verify(x => x.InsertAsync(It.IsAny<EventParticipant>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ValidEvent_UpdatesEvent()
        {
            // Arrange
            var eventItem = new Event { Id = Guid.NewGuid() };
            _eventRepositoryWrite.Setup(x => x.UpdateAsync(eventItem)).Returns(Task.CompletedTask);

            // Act
            await _eventService.UpdateAsync(eventItem);

            // Assert
            _eventRepositoryWrite.Verify(x => x.UpdateAsync(eventItem), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _eventRepositoryRead.Setup(x => x.GetAsync(id)).ReturnsAsync((Event)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _eventService.DeleteAsync(id));
        }

        [Fact]
        public async Task GetAsync_InvalidId_ThrowsException()
        {
            // Arrange
            var id = Guid.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _eventService.GetAsync(id));
        }

        [Fact]
        public async Task InsertAsync_InvalidEvent_ThrowsException()
        {
            // Arrange
            Event eventItem = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _eventService.InsertAsync(eventItem));
        }

        [Fact]
        public async Task InsertAsync_InvalidParticipant_ThrowsException()
        {
            // Arrange
            var eventItem = new Event { Id = Guid.NewGuid(), ParticipantId = Guid.NewGuid() };
            _participantRepositoryRead.Setup(x => x.AnyDataAsync(eventItem.ParticipantId)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _eventService.InsertAsync(eventItem));
        }

        [Fact]
        public async Task UpdateAsync_InvalidEvent_ThrowsException()
        {
            // Arrange
            Event eventItem = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _eventService.UpdateAsync(eventItem));
        }


    }

}
