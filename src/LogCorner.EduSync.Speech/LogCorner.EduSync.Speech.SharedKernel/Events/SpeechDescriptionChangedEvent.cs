using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechDescriptionChangedEvent : Event
    {
        public string Description { get; }

        public SpeechDescriptionChangedEvent(Guid aggregateId, string description)
        {
            AggregateId = aggregateId;
            Description = description;
        }
    }
}