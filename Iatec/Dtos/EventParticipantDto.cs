namespace Iatec.Dtos
{
    public class EventParticipantDto : ParticipantEventPost
    {
        public EventBaseDto EventBase { get; set; }
        public ParticipantDto ParticipantBase { get; set; }
    }
}
