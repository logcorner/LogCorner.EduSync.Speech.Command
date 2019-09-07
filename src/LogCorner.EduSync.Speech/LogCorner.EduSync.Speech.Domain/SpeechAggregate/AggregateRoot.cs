using LogCorner.EduSync.Speech.Domain.Events;
using System.Collections.Generic;
using System.Linq;
using LogCorner.EduSync.Speech.Domain.Exceptions;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public abstract class AggregateRoot<T> : Entity<T>, IEventSourcing
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        private readonly ICollection<IDomainEvent> _uncommittedEvents = new LinkedList<IDomainEvent>();

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
            if (!_uncommittedEvents.Any(x => Equals(x.EventId, @event.EventId)))
            {
                ((dynamic)this).Apply((dynamic)@event);
                _version = version;
            }
        }

        public IEnumerable<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsEnumerable();
        }

        public void ClearUncommittedEvents()
        {
            throw new System.NotImplementedException();
        }

        protected void AddDomainEvent(IDomainEvent newEvent)
        {
           // ValidateVersion(newEvent.Version);
           // newEvent.Version = ++_version;
         //  _domainEvents.Add(newEvent);
        }
    }
}