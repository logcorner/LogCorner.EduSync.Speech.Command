using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventSourcing
    {
        long Version { get; }

        void ValidateVersion(long expectedVersion);

        void ApplyEvent(IDomainEvent @event, long version);

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}