namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class NullInstanceOfAggregateIdException : AggregateException
    {
        public NullInstanceOfAggregateIdException(string message) : base(message)
        {
        }
    }
}