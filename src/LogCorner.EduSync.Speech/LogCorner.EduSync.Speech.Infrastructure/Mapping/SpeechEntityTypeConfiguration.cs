using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogCorner.EduSync.Speech.Infrastructure.Mapping
{
    internal class SpeechEntityTypeConfiguration : IEntityTypeConfiguration<Domain.SpeechAggregate.Speech>
    {
        public void Configure(EntityTypeBuilder<Domain.SpeechAggregate.Speech> speechEntityConfiguration)
        {
            speechEntityConfiguration.ToTable("Speech");
            // speechEntityConfiguration.Ignore(b => b.DomainEvents);
            speechEntityConfiguration.HasKey(o => o.Id);
            speechEntityConfiguration.OwnsOne(s => s.Url).Property(b => b.Value).HasColumnName("Url");
            speechEntityConfiguration.OwnsOne(s => s.Title).Property(b => b.Value).HasColumnName("Title");
            speechEntityConfiguration.OwnsOne(s => s.Description).Property(b => b.Value).HasColumnName("Description");
            speechEntityConfiguration.OwnsOne(s => s.Type).Property(b => b.Value).HasColumnName("Type");

            speechEntityConfiguration.HasMany(b => b.MediaFileItems);
        }
    }
}