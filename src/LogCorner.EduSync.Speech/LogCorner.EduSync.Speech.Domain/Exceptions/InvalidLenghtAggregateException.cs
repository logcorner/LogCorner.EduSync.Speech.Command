namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class InvalidLenghtAggregateException : AggregateException
    {
        public InvalidLenghtAggregateException(string message) : base(message)
        {
        }
    }
}