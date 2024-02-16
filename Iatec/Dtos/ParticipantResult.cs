using DomainLayer.Models;

namespace Iatec.Dtos
{
    public class ParticipantResult : ParticipantDto
    {
        /// <summary>
        /// Identifier of the participant
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// List of the events
        /// </summary>
        public IEnumerable<EventBaseDto> Events { get; set; }
    }
}
