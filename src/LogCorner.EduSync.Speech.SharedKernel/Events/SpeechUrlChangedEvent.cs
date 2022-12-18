using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechUrlChangedEvent : Event
    {
        public string Url { get; }

        public SpeechUrlChangedEvent(Guid aggregateId, string url, Guid eventId, DateTime ocurrendOn, long aggregateVersion = default)
        {
            AggregateId = aggregateId;
            Url = url;
            EventId = eventId;
            OcurrendOn = ocurrendOn;
            AggregateVersion = aggregateVersion;
        }
    }
}