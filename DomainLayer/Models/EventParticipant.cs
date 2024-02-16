namespace DomainLayer.Models
{
    public class EventParticipant
    {
        public Guid EventsId { get; set; }
        public Event Event { get; set; }
        public Guid ParticipantsId { get; set; }
        public Participant Participant { get; set; }
    }


}
