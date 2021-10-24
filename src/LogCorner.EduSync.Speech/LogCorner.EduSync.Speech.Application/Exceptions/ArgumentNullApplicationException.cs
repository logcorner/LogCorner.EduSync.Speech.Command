namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class ArgumentNullApplicationException : ApplicationException
    {
        private static int errorCode = Exceptions.ErrorCode.ArgumentNullApplicationException;

        public ArgumentNullApplicationException(string message) : base(errorCode, message)
        {
        }
    }
}