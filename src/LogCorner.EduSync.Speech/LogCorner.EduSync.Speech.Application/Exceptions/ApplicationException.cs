using LogCorner.EduSync.Speech.Domain.Exceptions;

namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class ApplicationException : BaseException
    {
        public ApplicationException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}