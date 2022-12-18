using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    [Serializable]
    public class ApplicationException : BaseException
    {
        protected ApplicationException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}