using LogCorner.EduSync.Speech.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using LogCorner.EduSync.Speech.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public abstract class AggregateRoot<T> : Entity<T>, IEventSourcing
    {
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
            _uncommittedEvents.Clear();
        }

        protected void AddDomainEvent(IDomainEvent @event, long originalVersion = -1)
        {
            ValidateVersion(originalVersion);
            @event.BuildVersion(_version + 1);
            ApplyEvent(@event, @event.AggregateVersion);
            _uncommittedEvents.Add(@event);
        }

        public void LoadFromHistory(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyEvent(@event, @event.AggregateVersion);
            }
        }
    }
}