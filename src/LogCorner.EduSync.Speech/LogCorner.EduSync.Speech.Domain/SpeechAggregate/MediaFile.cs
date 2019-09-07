using System;
using LogCorner.EduSync.Speech.Domain.Exceptions;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class MediaFile : Entity<Guid>
    {
        public UrlValue File { get; private set; }

        //EF Core need a parameterless constructor
        private MediaFile()
        {
        }

        public MediaFile(UrlValue file)
        {
            File = file ?? throw new ArgumentNullAggregateException(nameof(file));
        }
    }
}