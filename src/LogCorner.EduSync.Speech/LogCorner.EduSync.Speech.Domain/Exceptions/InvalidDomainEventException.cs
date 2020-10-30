namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidDomainEventException : AggregateException
    {
        public InvalidDomainEventException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}