namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidUrlAggregateException : AggregateException
    {
        public InvalidUrlAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}