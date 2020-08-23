using System;

namespace LogCorner.EduSync.Speech.SharedKernel.Events
{
    public class SpeechCreatedEvent : Event
    {
        public string Title { get; }
        public string Url { get; }
        public string Description { get; }
        public string Type { get; }

        public SpeechCreatedEvent(Guid id, string title, string url,
            string description, string type)
        {
            AggregateId = id;
            Title = title;
            Url = url;
            Description = description;
            Type = type;
        }
    }
}