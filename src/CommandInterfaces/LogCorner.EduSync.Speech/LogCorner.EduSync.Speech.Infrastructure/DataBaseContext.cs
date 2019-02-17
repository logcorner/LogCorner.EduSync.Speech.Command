using LogCorner.EduSync.Speech.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public virtual DbSet<Domain.SpeechAggregate.Speech> Speech { get; set; }
        public virtual DbSet<Domain.SpeechAggregate.MediaFile> MediaFile { get; set; }

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpeechEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MediaFileEntityTypeConfiguration());
        }
    }
}