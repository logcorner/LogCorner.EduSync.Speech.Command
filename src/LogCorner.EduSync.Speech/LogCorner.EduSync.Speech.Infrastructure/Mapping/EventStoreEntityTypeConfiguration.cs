using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogCorner.EduSync.Speech.Infrastructure.Mapping
{
    internal class EventStoreEntityTypeConfiguration : IEntityTypeConfiguration<EventStore>
    {
        public void Configure(EntityTypeBuilder<EventStore> eventStoreEntityConfiguration)
        {
            eventStoreEntityConfiguration.ToTable("EventStore")
                .HasKey(o => o.Id);
            eventStoreEntityConfiguration.Property(e => e.Version).IsRequired();
            eventStoreEntityConfiguration.Property(e => e.OccurredOn).IsRequired();
            eventStoreEntityConfiguration.Property(e => e.AggregateId).IsRequired();
            eventStoreEntityConfiguration.Property(e => e.Name).IsRequired();
            eventStoreEntityConfiguration.Property(e => e.SerializedBody).IsRequired()
                .HasColumnType("text");

            eventStoreEntityConfiguration.Property(e => e.TypeName).IsRequired()
                .HasMaxLength(250);
        }
    }
}