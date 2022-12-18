using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public abstract class Event : IDomainEvent
    {
        public Guid AggregateId { get; protected set; }

        public Guid EventId { get; protected set; }
        public long AggregateVersion { get; protected set; }
        public DateTime OcurrendOn { get; protected set; }

        public void BuildVersion(long aggregateVersion)
        {
            AggregateVersion = aggregateVersion;
        }
    }
}