using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        long AggregateVersion { get; }

        void BuildVersion(long aggregateVersion);
    }
}