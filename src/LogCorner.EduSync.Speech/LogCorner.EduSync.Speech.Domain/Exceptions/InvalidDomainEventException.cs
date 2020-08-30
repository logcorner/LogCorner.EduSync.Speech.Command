namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidDomainEventException : AggregateException
    {
        public InvalidDomainEventException(string message) : base(message)
        {
        }
    }
}