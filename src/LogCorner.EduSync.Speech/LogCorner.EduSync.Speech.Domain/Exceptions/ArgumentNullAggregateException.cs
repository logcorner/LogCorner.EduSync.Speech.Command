namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class ArgumentNullAggregateException : AggregateException
    {
        public ArgumentNullAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}