using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Presentation.Exceptions
{
    [Serializable]
    public class PresentationException : Exception
    {
        public PresentationException(string message) : base(message)
        {
        }

        protected PresentationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}