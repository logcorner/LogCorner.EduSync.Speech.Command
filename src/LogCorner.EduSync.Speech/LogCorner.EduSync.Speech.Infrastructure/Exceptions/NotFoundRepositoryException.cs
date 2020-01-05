namespace LogCorner.EduSync.Speech.Infrastructure.Exceptions
{
    public class NotFoundRepositoryException : RepositoryException
    {
        public NotFoundRepositoryException(string message) : base(message)
        {
        }
    }
}