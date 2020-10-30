using System;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; protected set; }

        public BaseException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}