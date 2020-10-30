using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class MediaFile : Entity<Guid>
    {
        public UrlValue File { get; private set; }

        //EF Core need a parameterless constructor
        private MediaFile()
        {
        }

        public MediaFile(Guid id, UrlValue file)
        {
            Id = id;
            File = file ?? throw new ArgumentNullAggregateException(0, nameof(file));
        }
    }
}