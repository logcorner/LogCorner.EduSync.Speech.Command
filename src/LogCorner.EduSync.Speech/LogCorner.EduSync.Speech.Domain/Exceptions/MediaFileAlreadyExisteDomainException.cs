namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class MediaFileAlreadyExistDomainException : DomainException
    {
        public MediaFileAlreadyExistDomainException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}