using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class AggregateException : DomainException
    {
        protected AggregateException(int errorCode, string message) : base(errorCode, message)
        {
            ErrorCode = errorCode;
        }

        protected AggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}