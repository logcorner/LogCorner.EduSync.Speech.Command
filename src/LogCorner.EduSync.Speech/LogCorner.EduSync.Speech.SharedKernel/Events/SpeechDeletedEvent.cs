using System;

namespace LogCorner.EduSync.Speech.SharedKernel.Events
{
    public class SpeechDeletedEvent : Event
    {
        public SpeechDeletedEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}