using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class ArgumentNullAggregateException : AggregateException
    {
        public ArgumentNullAggregateException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected ArgumentNullAggregateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}