using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class Speech : AggregateRoot<Guid>
    {
        public Title Title => new Title(_title);
        private string _title;

        private string _url;
        public UrlValue Url => new UrlValue(_url);

        private string _description;
        public Description Description => new Description(_description);

        private int _type;
        public SpeechType Type => new SpeechType(_type);

        private readonly List<MediaFile> _mediaFileItems;

        public IReadOnlyCollection<MediaFile> MediaFileItems => _mediaFileItems;
        public bool IsDeleted { get; private set; }

        //EF Core need a parameterless constructor
        private Speech()
        {
            _mediaFileItems = new List<MediaFile>();
        }

        public Speech(Title title, UrlValue urlValue, Description description, SpeechType type)
        {
            _title = title?.Value ?? throw new ArgumentNullAggregateException(0, nameof(title));
            _url = urlValue?.Value ?? throw new ArgumentNullAggregateException(0, nameof(urlValue));
            _description = description?.Value ?? throw new ArgumentNullAggregateException(0, nameof(description));
            _type = type != null ? type.IntValue : throw new ArgumentNullAggregateException(0, nameof(type));
            _mediaFileItems = new List<MediaFile>();
            AddDomainEvent(new SpeechCreatedEvent(Id, Title.Value, Url.Value, Description.Value, new SharedKernel.Events.SpeechTypeEnum(type.IntValue, type.StringValue)));
        }

        public Speech(Guid id, Title title, UrlValue urlValue, Description description, SpeechType type)
        {
            Id = id;
            _title = title.Value ?? throw new ArgumentNullAggregateException(0, nameof(title));
            _url = urlValue.Value ?? throw new ArgumentNullAggregateException(0, nameof(urlValue));
            _description = description.Value ?? throw new ArgumentNullAggregateException(0, nameof(description));
            _type = type.IntValue;
            _mediaFileItems = new List<MediaFile>();

            AddDomainEvent(new SpeechCreatedEvent(Id, Title.Value, Url.Value, Description.Value, new SharedKernel.Events.SpeechTypeEnum(type.IntValue, type.StringValue)));
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
                throw new ArgumentNullAggregateException(0, nameof(mediaFile));
            }

            if (_mediaFileItems.Contains(mediaFile))
            {
                throw new MediaFileAlreadyExistDomainException(ErrorCode.AlreadyExist, nameof(mediaFile));
            }

            AddDomainEvent(new MediaFileCreatedEvent(Id, mediaFile.Id, mediaFile.File.Value), originalVersion);
        }

        public void Apply(SpeechCreatedEvent ev)
        {
            Id = ev.AggregateId;
            _title = ev.Title;
            _url = ev.Url;
            _description = ev.Description;
            _type = ev.Type.Value;
        }

        public void Apply(MediaFileCreatedEvent ev)
        {
            if (_mediaFileItems.All(c => c.File.Value != ev.File))
            {
                _mediaFileItems.Add(new MediaFile(ev.MediaFileId, new UrlValue(ev.File)));
            }
        }

        #region - update title

        public void ChangeTitle(Title title, long originalVersion)
        {
            AddDomainEvent(new SpeechTitleChangedEvent(Id, title.Value), originalVersion);
        }

        public void ChangeDescription(Description description, long originalVersion)
        {
            AddDomainEvent(new SpeechDescriptionChangedEvent(Id, description.Value), originalVersion);
        }

        public void ChangeUrl(UrlValue url, long originalVersion)
        {
            AddDomainEvent(new SpeechUrlChangedEvent(Id, url.Value), originalVersion);
        }

        public void ChangeType(SpeechType type, long originalVersion)
        {
            AddDomainEvent(new SpeechTypeChangedEvent(Id, new SharedKernel.Events.SpeechTypeEnum(type.IntValue, type.StringValue)), originalVersion);
        }

        public void Apply(SpeechTitleChangedEvent ev)
        {
            if (ev.AggregateId != Id)
            {
                throw new InvalidDomainEventException(ErrorCode.InvalidDomainEvent, $"Cannot apply event : Speech Id ({Id}) is not equals to AggregateId ({ev.AggregateId}) of the event , {nameof(SpeechTitleChangedEvent)}");
            }
            _title = ev.Title;
        }

        public void Apply(SpeechDescriptionChangedEvent ev)
        {
            if (ev.AggregateId != Id)
            {
                throw new InvalidDomainEventException(ErrorCode.InvalidDomainEvent, $"Cannot apply event : Speech Id ({Id}) is not equals to AggregateId ({ev.AggregateId}) of the event , {nameof(SpeechDescriptionChangedEvent)}");
            }
            _description = ev.Description;
        }

        public void Apply(SpeechUrlChangedEvent ev)
        {
            if (ev.AggregateId != Id)
            {
                throw new InvalidDomainEventException(ErrorCode.InvalidDomainEvent, $"Cannot apply event : Speech Id ({Id}) is not equals to AggregateId ({ev.AggregateId}) of the event , {nameof(SpeechUrlChangedEvent)}");
            }
            _url = ev.Url;
        }

        public void Apply(SpeechTypeChangedEvent ev)
        {
            if (ev.AggregateId != Id)
            {
                throw new InvalidDomainEventException(ErrorCode.InvalidDomainEvent, $"Cannot apply event : Speech Id ({Id}) is not equals to AggregateId ({ev.AggregateId}) of the event , {nameof(SpeechTypeChangedEvent)}");
            }
            _type = new SpeechType(ev.Type.Name).IntValue;
        }

        #endregion - update title

        #region delete speech

        public void Delete(Guid id, long originalVersion)
        {
            if (id != Id)
            {
                throw new InvalidDomainEventException(ErrorCode.InvalidDomainEvent, $"Cannot apply event : Speech Id ({Id}) is not equals to AggregateId ({id}) of the event , {nameof(SpeechDeletedEvent)}");
            }
            AddDomainEvent(new SpeechDeletedEvent(Id, true), originalVersion);
        }

        public void Apply(SpeechDeletedEvent ev)
        {
            if (ev.AggregateId != Id)
            {
                throw new InvalidDomainEventException(ErrorCode.InvalidDomainEvent, $"Cannot apply event : Speech Id ({Id}) is not equals to AggregateId ({ev.AggregateId}) of the event , {nameof(SpeechDeletedEvent)}");
            }

            IsDeleted = ev.IsDeleted;
        }

        #endregion delete speech
    }
}