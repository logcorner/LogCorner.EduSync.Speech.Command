using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechUrlChangedEvent : Event
    {
        public string Url { get; }

        public SpeechUrlChangedEvent(Guid aggregateId, string url)
        {
            AggregateId = aggregateId;
            Url = url;
        }
    }
}