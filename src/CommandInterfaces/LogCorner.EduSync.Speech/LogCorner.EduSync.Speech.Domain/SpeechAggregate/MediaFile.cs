using LogCorner.EduSync.Speech.Domain.Exceptions;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class MediaFile : Entity<int>
    {
        public UrlValue File { get; private set; }

        public MediaFile(UrlValue file)
        {
            File = file ?? throw new ArgumentNullAggregateException(nameof(file));
        }
    }
}