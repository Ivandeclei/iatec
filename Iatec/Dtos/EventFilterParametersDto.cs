using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class EventFilterParametersDto : PaginateFilterDto
    {
        /// <summary>
        /// Date of the event to search for
        /// </summary>
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "DataEvent field must be in the 'yyyy-MM-dd' format")]
        public DateTime? DateEvent { get; set; }

        /// <summary>
        /// Hour of Event to search for
        /// </summary>
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Time field must be in the 'HH:mm' format")]
        public string? HourEvent { get; set; }

        /// <summary>
        /// Name of the event to search for
        /// </summary>
        public string? EventName { get; set; }

        /// <summary>
        /// Description of the event to search for
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Represents the day to be searched for events
        /// </summary>
        public int? Day { get; set; }

        /// <summary>
        /// Represents the Month to be searched for events
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Represents the Week to be searched for events
        /// </summary>
        public bool? Week { get; set; }
    }
}
