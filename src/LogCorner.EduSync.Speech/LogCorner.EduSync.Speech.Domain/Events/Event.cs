using System;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public abstract class Event : IDomainEvent
    {
        public Guid AggregateId { get; protected set; }

        public Guid EventId => Guid.NewGuid();

        public DateTime OcurrendOn => DateTime.UtcNow;
    }
}

