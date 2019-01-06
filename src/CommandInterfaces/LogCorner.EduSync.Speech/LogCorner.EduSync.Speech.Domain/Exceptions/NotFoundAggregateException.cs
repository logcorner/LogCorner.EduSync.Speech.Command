namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class NotFoundAggregateException : AggregateException
    {
        public NotFoundAggregateException(string message) : base(message)
        {
        }
    }
}