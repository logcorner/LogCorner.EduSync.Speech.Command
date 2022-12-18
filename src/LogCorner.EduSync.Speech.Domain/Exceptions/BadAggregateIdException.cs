using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class BadAggregateIdException : AggregateException
    {
        public BadAggregateIdException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected BadAggregateIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}