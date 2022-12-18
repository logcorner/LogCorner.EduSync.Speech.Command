using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class InvalidUrlAggregateException : AggregateException
    {
        public InvalidUrlAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected InvalidUrlAggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}