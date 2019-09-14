namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class AggregateException : DomainException
    {
        protected AggregateException(string message) : base(message)
        {
        }
    }
}