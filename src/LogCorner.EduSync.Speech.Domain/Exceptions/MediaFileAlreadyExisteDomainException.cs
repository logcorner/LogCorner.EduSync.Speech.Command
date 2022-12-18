using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class MediaFileAlreadyExistDomainException : DomainException
    {
        public MediaFileAlreadyExistDomainException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected MediaFileAlreadyExistDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}