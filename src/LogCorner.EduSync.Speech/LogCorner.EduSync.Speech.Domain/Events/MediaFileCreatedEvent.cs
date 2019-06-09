using System;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public class MediaFileCreatedEvent : DomainEvent
    {
        public UrlValue File { get; }

        public MediaFileCreatedEvent(Guid id, UrlValue file, int version)
        {
            Id = id.ToString();
            File = file;
            Version = version;
        }
    }
}