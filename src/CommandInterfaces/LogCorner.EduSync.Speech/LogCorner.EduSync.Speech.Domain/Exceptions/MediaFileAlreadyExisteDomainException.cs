namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class MediaFileAlreadyExisteDomainException : DomainException
    {
        public MediaFileAlreadyExisteDomainException(string message) : base(message)
        {
        }
    }
}