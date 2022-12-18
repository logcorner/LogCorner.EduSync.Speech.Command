using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public int ErrorCode { get; protected init; }

        protected BaseException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}