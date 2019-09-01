namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidVersionAggregateException : AggregateException
    {
        public InvalidVersionAggregateException(string message) : base(message)
        {
        }
    }
}