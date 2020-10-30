namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidEnumAggregateException : AggregateException
    {
        public InvalidEnumAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}