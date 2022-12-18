using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechDeletedEvent : Event
    {
        public bool IsDeleted { get; }

        public SpeechDeletedEvent(Guid aggregateId, bool isDeleted, Guid eventId, DateTime ocurrendOn, long aggregateVersion = default)
        {
            AggregateId = aggregateId;
            IsDeleted = isDeleted;
            EventId = eventId;
            OcurrendOn = ocurrendOn;
            AggregateVersion = aggregateVersion;
        }
    }
}