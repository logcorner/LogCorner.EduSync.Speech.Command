using System;

namespace LogCorner.EduSync.Speech.SharedKernel.Events
{
    public class SpeechDeletedEvent : Event
    {
        public bool IsDeleted { get; }

        public SpeechDeletedEvent(Guid aggregateId, bool isDeleted)
        {
            AggregateId = aggregateId;
            IsDeleted = isDeleted;
        }
    }
}