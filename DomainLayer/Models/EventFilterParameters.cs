namespace DomainLayer.Models
{
    public class EventFilterParameters : PaginateFilter
    {
        public DateTime? DateEvent { get; set; }
        public string? HourEvent { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public bool? Week { get; set; }

    }
}
