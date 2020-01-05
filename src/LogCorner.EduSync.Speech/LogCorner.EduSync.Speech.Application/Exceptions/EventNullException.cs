namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class EventNullException : ArgumentNullApplicationException
    {
        internal EventNullException(string message) : base(message)
        {
        }
    }
}