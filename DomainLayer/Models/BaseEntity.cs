namespace DomainLayer.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
