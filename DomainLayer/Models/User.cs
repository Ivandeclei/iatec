namespace DomainLayer.Models
{
    public class User : BaseEntity
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}
