namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class ConcurrencyException : AggregateException
    {
        public ConcurrencyException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}