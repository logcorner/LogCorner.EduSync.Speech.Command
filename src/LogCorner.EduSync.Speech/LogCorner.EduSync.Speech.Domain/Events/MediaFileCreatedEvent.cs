using System;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public class MediaFileCreatedEvent : Event
    {
        public UrlValue File { get; }
        public Guid MediaFileId { get; }

        public MediaFileCreatedEvent(Guid aggregateId, Guid mediaFileId, UrlValue file)
        {
            MediaFileId = mediaFileId;
            AggregateId = aggregateId;
            File = file;
        }
    }
}