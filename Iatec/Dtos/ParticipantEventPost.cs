using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class ParticipantEventPost
    {
        /// <summary>
        /// Identifier of the event
        /// </summary>
        [Required(ErrorMessage = "EventsId field is required")]
        public Guid EventsId { get; set; }

        /// <summary>
        /// Identifier of the participant
        /// </summary>
        [Required(ErrorMessage = "ParticipantsId field is required")]
        public Guid ParticipantsId { get; set; }
    }
}
