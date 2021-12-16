using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Domain.SpeechAggregate.Speech> Speech { get; set; }
        public DbSet<MediaFile> MediaFile { get; set; }

        public DbSet<EventStore> EventStore { get; set; }

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpeechEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MediaFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventStoreEntityTypeConfiguration());
        }
    }
}