using System;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public class SpeechCreatedEvent : Event
    {
        public Title Title { get; }
        public UrlValue Url { get; }
        public Description Description { get; }
        public SpeechType Type { get; }

        public SpeechCreatedEvent(Guid id, Title title, UrlValue url,
                                  Description description, SpeechType type)
        {
            AggregateId = id;
            Title = title;
            Url = url;
            Description = description;
            Type = type;
        }
    }
}