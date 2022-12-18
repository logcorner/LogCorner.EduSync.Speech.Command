using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechTypeChangedEvent : Event
    {
        public SpeechTypeEnum Type { get; }

        public SpeechTypeChangedEvent(Guid aggregateId, SpeechTypeEnum type, Guid eventId, DateTime ocurrendOn, long aggregateVersion = default)
        {
            AggregateId = aggregateId;
            Type = type;
            EventId = eventId;
            OcurrendOn = ocurrendOn;
            AggregateVersion = aggregateVersion;
        }
    }
}