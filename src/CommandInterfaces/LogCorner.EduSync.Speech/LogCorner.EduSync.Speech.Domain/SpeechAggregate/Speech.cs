using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class Speech : AggregateRoot<Guid>
    {
        public Title Title { get; private set; }
        public UrlValue Url { get; private set; }
        public Description Description { get; private set; }
        public SpeechType Type { get; private set; }

        public Speech(Title title, UrlValue urlValue, Description description, SpeechType type)
        {
            Title = title ?? throw new ArgumentNullAggregateException(nameof(title));
            Url = urlValue ?? throw new ArgumentNullAggregateException(nameof(urlValue));
            Description = description ?? throw new ArgumentNullAggregateException(nameof(description));
            Type = type ?? throw new ArgumentNullAggregateException(nameof(type));
            AddDomainEvent(new SpeechCreateEvent(Id, Title, Url, Description, Type));
        }

        public Speech(Guid id, Title title, UrlValue urlValue, Description description, SpeechType type)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullAggregateException(nameof(title));
            Url = urlValue ?? throw new ArgumentNullAggregateException(nameof(urlValue));
            Description = description ?? throw new ArgumentNullAggregateException(nameof(description));
            Type = type ?? throw new ArgumentNullAggregateException(nameof(type));
            AddDomainEvent(new SpeechCreateEvent(Id, Title, Url, Description, Type));
        }

        /*
         Speech will need medias, organizer, talker, etc...
         I will implement them in other features when it is necessary
         KISS : Keep It Simple, Stupid
         YAGNI : ou Aren’t Gonna Need It
         */
    }
}