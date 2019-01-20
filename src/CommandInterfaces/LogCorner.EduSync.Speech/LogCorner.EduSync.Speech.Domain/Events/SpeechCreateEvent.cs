using System;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public class SpeechCreatedEvent : DomainEvent
    {
        public Title Title { get; }
        public UrlValue Url { get; }
        public Description Description { get; }
        public SpeechType Type { get; }

        public SpeechCreatedEvent(Guid id, Title title, UrlValue url, Description description, SpeechType type)
        {
            Id = id.ToString();
            Title = title;
            Url = url;
            Description = description;
            Type = type;
        }
    }
}