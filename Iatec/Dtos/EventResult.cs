namespace Iatec.Dtos
{
    public class EventResult : EventBaseDto
    {
        /// <summary>
        /// Identifir of the event
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// List of the participants
        /// </summary>
        public IEnumerable<ParticipantDto> Participants { get; set; }
    }
}
