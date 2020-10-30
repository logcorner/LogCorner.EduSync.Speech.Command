namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        protected DomainException(int errorCode, string message) : base(errorCode, message)
        {
        }
    }
}