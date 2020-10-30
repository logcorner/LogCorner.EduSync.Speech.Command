namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class BadAggregateIdException : AggregateException
    {
        public BadAggregateIdException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}