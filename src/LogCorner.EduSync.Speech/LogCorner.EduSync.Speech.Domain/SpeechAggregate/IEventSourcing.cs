﻿using System.Collections.Generic;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

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