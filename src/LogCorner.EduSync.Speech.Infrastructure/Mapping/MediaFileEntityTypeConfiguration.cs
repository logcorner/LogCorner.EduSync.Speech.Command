using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogCorner.EduSync.Speech.Infrastructure.Mapping
{
    internal class MediaFileEntityTypeConfiguration : IEntityTypeConfiguration<MediaFile>
    {
        public void Configure(EntityTypeBuilder<MediaFile> speechEntityConfiguration)
        {
            speechEntityConfiguration.ToTable("MediaFile");
            speechEntityConfiguration.HasKey(o => o.Id);
            speechEntityConfiguration.OwnsOne(s => s.File).Property(b => b.Value).HasColumnName("Url");
        }
    }
}