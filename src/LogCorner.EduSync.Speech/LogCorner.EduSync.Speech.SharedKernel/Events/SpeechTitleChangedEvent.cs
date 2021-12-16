using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechTitleChangedEvent : Event
    {
        public string Title { get; }

        public SpeechTitleChangedEvent(Guid aggregateId, string title)
        {
            AggregateId = aggregateId;
            Title = title;
        }
    }
}