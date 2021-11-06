using LogCorner.EduSync.Speech.SharedKernel.Events;
using System;

namespace LogCorner.EduSync.Speech.Domain.UnitTest.Specs
{
    public class SubEvent : IDomainEvent
    {
        public Guid EventId { get; }
        public long AggregateVersion { get; private set; } = -1;
        public Guid AggregateId { get; }
        public object Value { get; }

        public SubEvent(Guid eventId, Guid aggregateId, object value)
        {
            EventId = eventId;
            AggregateId = aggregateId;
            Value = value;
        }

        public void BuildVersion(long aggregateVersion)
        {
            AggregateVersion = aggregateVersion;
        }
    }
}