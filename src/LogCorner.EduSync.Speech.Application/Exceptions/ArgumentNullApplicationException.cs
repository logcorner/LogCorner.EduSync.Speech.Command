using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    [Serializable]
    public class ArgumentNullApplicationException : ApplicationException
    {
        private static int _errorCode = Exceptions.ErrorCode.ArgumentNullApplicationException;

        public ArgumentNullApplicationException(string message) : base(_errorCode, message)
        {
        }

        protected ArgumentNullApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}