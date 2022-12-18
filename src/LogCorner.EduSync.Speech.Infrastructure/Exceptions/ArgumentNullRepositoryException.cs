using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Infrastructure.Exceptions
{
    [Serializable]
    public class ArgumentNullRepositoryException : RepositoryException
    {
        public ArgumentNullRepositoryException(string message) : base(message)
        {
        }

        protected ArgumentNullRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}