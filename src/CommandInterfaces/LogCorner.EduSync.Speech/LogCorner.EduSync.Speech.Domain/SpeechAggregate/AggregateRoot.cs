using System.Collections.Generic;
using LogCorner.EduSync.Speech.Domain.Events;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        protected AggregateRoot()
        {
        }

        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(DomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }
    }
}