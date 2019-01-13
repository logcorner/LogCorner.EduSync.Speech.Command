using System;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public class SpeechCreateEvent : DomainEvent
    {
        public Title Title { get; }
        public UrlValue Url { get; }
        public Description Description { get; }
        public SpeechType Type { get; }

        public SpeechCreateEvent(Guid id, Title title, UrlValue url, Description description, SpeechType type)
        {
            Id = id.ToString();
            Title = title;
            Url = url;
            Description = description;
            Type = type;
        }
    }
}