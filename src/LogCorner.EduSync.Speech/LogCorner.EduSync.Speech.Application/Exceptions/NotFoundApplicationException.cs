namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class NotFoundApplicationException : ApplicationException
    {
        internal NotFoundApplicationException(string message) : base(message)
        {
        }
    }
}