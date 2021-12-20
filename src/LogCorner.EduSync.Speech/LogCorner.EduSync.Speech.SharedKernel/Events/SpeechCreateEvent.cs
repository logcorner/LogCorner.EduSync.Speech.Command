using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechCreatedEvent : Event
    {
        public string Title { get; }
        public string Url { get; }
        public string Description { get; }
        public SpeechTypeEnum Type { get; }

        public SpeechCreatedEvent(Guid id, string title, string url,
            string description, SpeechTypeEnum type)
        {
            AggregateId = id;
            Title = title;
            Url = url;
            Description = description;
            Type = type;
        }
    }
}