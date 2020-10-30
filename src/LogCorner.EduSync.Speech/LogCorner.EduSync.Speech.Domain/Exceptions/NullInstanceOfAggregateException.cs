namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class NullInstanceOfAggregateException : AggregateException
    {
        public NullInstanceOfAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}