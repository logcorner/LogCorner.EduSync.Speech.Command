using LogCorner.EduSync.Speech.Domain.Events;
using System.Collections.Generic;
using LogCorner.EduSync.Speech.Domain.Exceptions;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public abstract class AggregateRoot<T> : Entity<T>, IEventSourcing
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

        public long Version => _version;
        private long _version = -1;

        protected AggregateRoot()
        {
        }

        public void ValidateVersion(long expectedVersion)
        {
            if (Version != expectedVersion)
            {
                throw new ConcurrencyException(
                    $@"Invalid version specified : expectedVersion = {Version}
                          but  originalVersion = {expectedVersion}.");
            }
        }

        public void ApplyEvent(IDomainEvent @event, long version)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            throw new System.NotImplementedException();
        }

        public void ClearUncommittedEvents()
        {
            throw new System.NotImplementedException();
        }

        protected void AddDomainEvent(DomainEvent newEvent)
        {
           // ValidateVersion(newEvent.Version);
            newEvent.Version = ++_version;
            _domainEvents.Add(newEvent);
        }
    }
}