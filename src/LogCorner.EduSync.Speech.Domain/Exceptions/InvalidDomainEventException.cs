using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    [Serializable]
    public class InvalidDomainEventException : AggregateException
    {
        public InvalidDomainEventException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected InvalidDomainEventException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}