using DomainLayer.EntityMapper;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<EventParticipant> EventParticipant { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {     
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ParticipantMap());
            modelBuilder.ApplyConfiguration(new EventMap());

            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);

            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventsId, ep.ParticipantsId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne<Event>(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventsId);

            modelBuilder.Entity<EventParticipant>()
                .HasOne<Participant>(ep => ep.Participant)
                .WithMany(p => p.Events)
                .HasForeignKey(ep => ep.ParticipantsId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
