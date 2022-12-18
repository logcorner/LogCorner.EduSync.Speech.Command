using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class NullInstanceOfAggregateException : AggregateException
    {
        public NullInstanceOfAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected NullInstanceOfAggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}