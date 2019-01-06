using System;

namespace LogCorner.EduSync.Speech.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        internal ApplicationException(string message) : base(message)
        {
        }
    }
}