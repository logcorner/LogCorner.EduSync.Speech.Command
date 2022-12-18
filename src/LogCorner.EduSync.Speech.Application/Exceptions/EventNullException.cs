using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    [Serializable]
    public class EventNullException : ArgumentNullApplicationException
    {
        public EventNullException(string message) : base(message)
        {
        }

        protected EventNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}