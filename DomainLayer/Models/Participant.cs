namespace DomainLayer.Models
{
    public class Participant : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<EventParticipant> Events { get; set; }
        public User User { get; set; }
    }
}
