namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class EventNullException : ArgumentNullApplicationException
    {
        public EventNullException(string message) : base(message)
        {
        }
    }
}