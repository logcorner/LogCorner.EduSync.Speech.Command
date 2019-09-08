using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;

namespace LogCorner.EduSync.Speech.Domain.UnitTest
{
    public class SubEvent : IDomainEvent
    {
        public Guid EventId { get; }
        public long AggregateVersion { get; private set; } = -1;

        public void BuildVersion(long aggregateVersion)
        {
            AggregateVersion = aggregateVersion;
        }
    }
}