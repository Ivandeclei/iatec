using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer.EntityMapper
{
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.EventPlace).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.DateEvent).IsRequired();
            builder.Property(x => x.TypeEvent).IsRequired();
            builder.Property(x => x.ParticipantId).IsRequired();
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.ModifiedDate);

        }
    }
}
