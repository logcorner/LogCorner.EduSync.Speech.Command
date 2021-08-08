namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class NotFoundApplicationException : ApplicationException
    {
        private static int errorCode = Exceptions.ErrorCode.NotFoundApplicationException;

        public NotFoundApplicationException(string message) : base(errorCode, message)
        {
        }
    }
}