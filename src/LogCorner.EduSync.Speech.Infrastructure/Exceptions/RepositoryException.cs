using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Infrastructure.Exceptions
{
    [Serializable]
    public class RepositoryException : InfrastructureException
    {
        protected RepositoryException(string message) : base(message)
        {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}