using System;

namespace LogCorner.EduSync.Speech.SharedKernel.Events
{
    public class SpeechTypeChangedEvent : Event
    {
        public string Type { get; }

        public SpeechTypeChangedEvent(Guid aggregateId, string type)
        {
            AggregateId = aggregateId;
            Type = type;
        }
    }
}