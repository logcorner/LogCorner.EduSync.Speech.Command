using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventSourcing
    {
        long Version { get; }

        void ValidateVersion(long expectedVersion);

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();
    }
}