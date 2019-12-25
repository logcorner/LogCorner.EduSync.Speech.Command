namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class ApplicationNotFoundException : ApplicationException
    {
        internal ApplicationNotFoundException(string message) : base(message)
        {
        }
    }
}