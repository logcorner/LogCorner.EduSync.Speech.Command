using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class MediaFileCreatedEvent : Event
    {
        public string File { get; }
        public Guid MediaFileId { get; }

        public MediaFileCreatedEvent(Guid aggregateId, Guid mediaFileId, string file)
        {
            MediaFileId = mediaFileId;
            AggregateId = aggregateId;
            File = file;
        }
    }
}