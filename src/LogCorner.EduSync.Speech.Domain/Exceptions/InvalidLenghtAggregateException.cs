using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class InvalidLenghtAggregateException : AggregateException
    {
        public InvalidLenghtAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected InvalidLenghtAggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}