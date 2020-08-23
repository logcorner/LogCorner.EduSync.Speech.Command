using System;

namespace LogCorner.EduSync.Speech.SharedKernel.Events
{
    public abstract class Event : IDomainEvent
    {
        public Guid AggregateId { get; protected set; }

        public Guid EventId => Guid.NewGuid();
        public long AggregateVersion { get; private set; }
        public DateTime OcurrendOn => DateTime.UtcNow;

        public void BuildVersion(long aggregateVersion)
        {
            AggregateVersion = aggregateVersion;
        }
    }
}