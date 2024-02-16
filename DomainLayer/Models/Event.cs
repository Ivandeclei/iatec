using DomainLayer.Models.@enum;

namespace DomainLayer.Models
{
    public class Event : BaseEntity
    {
        public Guid ParticipantId { get; set; }
        public IEnumerable<EventParticipant> Participants { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EventPlace { get; set; }
        public EventTypeEnum TypeEvent { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DateEvent { get; set; }
    }
}
