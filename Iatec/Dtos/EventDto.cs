namespace Iatec.Dtos
{
    public class EventDto : EventBaseDto
    {
        /// <summary>
        /// List of the identifier with id
        /// </summary>
        public IEnumerable<EventParticipantDto> EventParticipant { get; set; }
        
    }
}
