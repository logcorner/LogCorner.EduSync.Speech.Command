using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class InvalidEnumAggregateException : AggregateException
    {
        public InvalidEnumAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected InvalidEnumAggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}