using AutoFixture;
using DomainLayer.Models.@enum;
using DomainLayer.Models;
using DomainServiceLayer;
using DomainServiceLayer.CustomExceptionService;
using DomainServiceLayer.Interfaces;
using InfrastructureLayer.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DomainService.Tests
{
    public class ParticipantEventServiceTests
    {
        private Mock<IParticipantEventRepositoryWrite> _participantEventRepositoryWrite;
        private Mock<IEventRepositoryRead> _eventRepositoryRead;
        private IParticipantEventService _participantEventService;
        private Fixture _fixture;

        public ParticipantEventServiceTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _participantEventRepositoryWrite = new Mock<IParticipantEventRepositoryWrite>();
            _eventRepositoryRead = new Mock<IEventRepositoryRead>();

            _participantEventService = new ParticipantEventService(_participantEventRepositoryWrite.Object, _eventRepositoryRead.Object);
        }

        [Fact]
        public async Task InsertAsync_ShouldInsertEventParticipant_WhenEventIsActiveAndShared()
        {
            // Arrange
            var eventParticipant = _fixture.Create<EventParticipant>();
            var eventItem = _fixture.Build<Event>()
                .With(e => e.Status, StatusEnum.Active)
                .With(e => e.TypeEvent, EventTypeEnum.Shared)
                .Create();

            _eventRepositoryRead.Setup(x => x.GetAsync(eventParticipant.EventsId)).ReturnsAsync(eventItem);

            // Act
            await _participantEventService.InsertAsync(eventParticipant);

            // Assert
            _participantEventRepositoryWrite.Verify(x => x.InsertAsync(eventParticipant), Times.Once);
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowException_WhenEventIsNotActive()
        {
            // Arrange
            var eventParticipant = _fixture.Create<EventParticipant>();
            var eventItem = _fixture.Build<Event>()
                .With(e => e.Status, StatusEnum.Desabled)
                .With(e => e.TypeEvent, EventTypeEnum.Shared)
                .Create();

            _eventRepositoryRead.Setup(x => x.GetAsync(eventParticipant.EventsId)).ReturnsAsync(eventItem);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _participantEventService.InsertAsync(eventParticipant));
        }

        [Fact]
        public async Task InsertAsync_ShouldThrowException_WhenEventIsNotShared()
        {
            // Arrange
            var eventParticipant = _fixture.Create<EventParticipant>();
            var eventItem = _fixture.Build<Event>()
                .With(e => e.Status, StatusEnum.Active)
                .With(e => e.TypeEvent, EventTypeEnum.Exclusive)
                .Create();

            _eventRepositoryRead.Setup(x => x.GetAsync(eventParticipant.EventsId)).ReturnsAsync(eventItem);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(() => _participantEventService.InsertAsync(eventParticipant));
        }
    }

}
