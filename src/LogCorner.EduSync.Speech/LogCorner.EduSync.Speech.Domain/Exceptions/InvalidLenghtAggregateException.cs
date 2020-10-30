namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidLenghtAggregateException : AggregateException
    {
        public InvalidLenghtAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}