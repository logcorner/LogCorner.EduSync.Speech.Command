using System;

namespace LogCorner.EduSync.Speech.SharedKernel.Events
{
    public class SpeechTypeChangedEvent : Event
    {
        public SpeechTypeEnum Type { get; }

        public SpeechTypeChangedEvent(Guid aggregateId, SpeechTypeEnum type)
        {
            AggregateId = aggregateId;
            Type = type;
        }
    }
}