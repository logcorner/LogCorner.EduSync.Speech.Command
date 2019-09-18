namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class BadAggregateIdException : AggregateException
    {
        public BadAggregateIdException(string message) : base(message)
        {
        }
    }
}