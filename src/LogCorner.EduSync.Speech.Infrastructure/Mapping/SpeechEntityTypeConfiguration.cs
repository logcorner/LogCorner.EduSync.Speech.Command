using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogCorner.EduSync.Speech.Infrastructure.Mapping
{
    internal class SpeechEntityTypeConfiguration : IEntityTypeConfiguration<Domain.SpeechAggregate.Speech>
    {
        public void Configure(EntityTypeBuilder<Domain.SpeechAggregate.Speech> speechEntityConfiguration)
        {
            speechEntityConfiguration.ToTable("Speech");
            speechEntityConfiguration.HasKey(o => o.Id);
            speechEntityConfiguration.Property<string>("_url").IsRequired().HasColumnName("Url");
            speechEntityConfiguration.Property<string>("_title").IsRequired().HasColumnName("Title");
            speechEntityConfiguration.Property<string>("_description").IsRequired().HasColumnName("Description");
            speechEntityConfiguration.Property<int>("_type").IsRequired().HasColumnName("Type");

            speechEntityConfiguration.HasMany(b => b.MediaFileItems);
        }
    }
}