using DomainLayer.Models.@enum;
using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class EventBaseDto : BaseEntityDto
    {
        /// <summary>
        /// Identifier of the user who created the event
        /// </summary>
        [Required(ErrorMessage = "ParticipantId field is required")]
        public Guid ParticipantId { get; set; }

        /// <summary>
        /// Name of event
        /// </summary>
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }

        /// <summary>
        /// Description of event
        /// </summary>
        [Required(ErrorMessage = "Description field is required")]
        public string Description { get; set; }

        /// <summary>
        /// Place of event
        /// </summary>
        [Required(ErrorMessage = "EventPlace field is required")]
        public string EventPlace { get; set; }

        /// <summary>
        /// Type of event 
        /// </summary>
        /// <example>Shared = 1</example>
        /// <example>Exclusive = 0</example>
        [Required(ErrorMessage = "O campo TypeEvent é obrigatório")]
        [EnumDataType(typeof(EventTypeEnum), ErrorMessage = "Valid values for TypeEvent are 0 (Exclusive) or 1 (Shared)")]
        public EventTypeEnum TypeEvent { get; set; }

        /// <summary>
        /// Status that the event can have
        /// </summary>
        ///  <example>Desabled = 0</example>
        ///  <example>Active = 1</example>
        [Required(ErrorMessage = "O campo Status é obrigatório")]
        [EnumDataType(typeof(StatusEnum), ErrorMessage = "Valid values for status are 0 (Desabled) or 1 (Active)")]
        public StatusEnum Status { get; set; }

        /// <summary>
        /// Date of the event
        /// </summary>
        /// <example>2024-02-16T02:17:31.136Z</example>
        [Required(ErrorMessage = "DateEvent field is required")]
        public DateTime DateEvent { get; set; }
    }
}
