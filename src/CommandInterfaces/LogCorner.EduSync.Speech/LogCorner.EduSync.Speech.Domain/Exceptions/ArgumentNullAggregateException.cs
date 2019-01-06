namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class ArgumentNullAggregateException : AggregateException
    {
        public ArgumentNullAggregateException(string message) : base(message)
        {
        }
    }
}