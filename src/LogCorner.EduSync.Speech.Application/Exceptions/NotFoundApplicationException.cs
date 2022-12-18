using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    [Serializable]
    public class NotFoundApplicationException : ApplicationException
    {
        private static int _errorCode = Exceptions.ErrorCode.NotFoundApplicationException;

        public NotFoundApplicationException(string message) : base(_errorCode, message)
        {
        }

        protected NotFoundApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}