using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class Speech : AggregateRoot<Guid>
    {
        public Title Title { get; private set; }
        public UrlValue Url { get; private set; }
        public Description Description { get; private set; }
        public SpeechType Type { get; private set; }

        private readonly List<MediaFile> _mediaFileItems;
        public IReadOnlyCollection<MediaFile> MediaFileItems => _mediaFileItems;

        //EF Core need a parameterless constructor
        private Speech()
        {
        }

        public Speech(Title title, UrlValue urlValue, Description description, SpeechType type)
        {
            Title = title ?? throw new ArgumentNullAggregateException(nameof(title));
            Url = urlValue ?? throw new ArgumentNullAggregateException(nameof(urlValue));
            Description = description ?? throw new ArgumentNullAggregateException(nameof(description));
            Type = type ?? throw new ArgumentNullAggregateException(nameof(type));
            _mediaFileItems = new List<MediaFile>();
            AddDomainEvent(new SpeechCreatedEvent(Id, Title, Url, Description, Type));
        }

        public Speech(Guid id, Title title, UrlValue urlValue, Description description, SpeechType type)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullAggregateException(nameof(title));
            Url = urlValue ?? throw new ArgumentNullAggregateException(nameof(urlValue));
            Description = description ?? throw new ArgumentNullAggregateException(nameof(description));
            Type = type ?? throw new ArgumentNullAggregateException(nameof(type));
            _mediaFileItems = new List<MediaFile>();

            AddDomainEvent(new SpeechCreatedEvent(Id, Title, Url, Description, Type));
        }

        /*
         Speech will need medias, organizer, talker, etc...
         I will implement them in other features when it is necessary
         KISS : Keep It Simple, Stupid
         YAGNI : You Aren’t Gonna Need It
         */

        public void CreateMedia(MediaFile mediaFile, int originalVersion)
        {
            if (mediaFile == null)
            {
                throw new ArgumentNullAggregateException(nameof(mediaFile));
            }

            if (_mediaFileItems.Contains(mediaFile))
            {
                throw new MediaFileAlreadyExisteDomainException(nameof(mediaFile));
            }
            _mediaFileItems.Add(mediaFile);

            AddDomainEvent(new MediaFileCreatedEvent(Id, mediaFile.Id, mediaFile.File),originalVersion);
        }

        public void Apply(SpeechCreatedEvent ev)
        {
            Id = ev.AggregateId;
            Title = ev.Title;
            Url = ev.Url;
            Description = ev.Description;
            Type = ev.Type;
        }
    }
}