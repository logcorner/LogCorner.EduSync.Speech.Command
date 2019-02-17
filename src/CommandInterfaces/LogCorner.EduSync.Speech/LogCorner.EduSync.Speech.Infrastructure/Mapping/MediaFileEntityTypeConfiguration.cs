using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogCorner.EduSync.Speech.Infrastructure.Mapping
{
    internal class MediaFileEntityTypeConfiguration : IEntityTypeConfiguration<Domain.SpeechAggregate.MediaFile>
    {
        public void Configure(EntityTypeBuilder<Domain.SpeechAggregate.MediaFile> speechEntityConfiguration)
        {
            speechEntityConfiguration.ToTable("MediaFile");
            speechEntityConfiguration.HasKey(o => o.Id);
            speechEntityConfiguration.OwnsOne(s => s.File).Property(b => b.Value).HasColumnName("Url");
        }
    }
}