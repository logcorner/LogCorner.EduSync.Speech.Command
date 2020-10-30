namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class AggregateException : DomainException
    {
        protected AggregateException(int errorCode, string message) : base(errorCode, message)
        {
            ErrorCode = errorCode;
        }
    }
}