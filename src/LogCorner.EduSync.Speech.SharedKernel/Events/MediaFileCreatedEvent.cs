using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class MediaFileCreatedEvent : Event
    {
        public string File { get; }
        public Guid MediaFileId { get; }

        public MediaFileCreatedEvent(Guid aggregateId, Guid mediaFileId, string file, Guid eventId, DateTime ocurrendOn, long aggregateVersion = default) 
        {
            MediaFileId = mediaFileId;
            AggregateId = aggregateId;
            File = file;
            EventId = eventId;
            OcurrendOn = ocurrendOn;
            AggregateVersion = aggregateVersion;
        }
    }
}