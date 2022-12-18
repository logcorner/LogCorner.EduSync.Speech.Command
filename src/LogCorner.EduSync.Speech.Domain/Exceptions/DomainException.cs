using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class DomainException : BaseException
    {
        protected DomainException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}