using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        long AggregateVersion { get; }

        void BuildVersion(long aggregateVersion);
    }
}