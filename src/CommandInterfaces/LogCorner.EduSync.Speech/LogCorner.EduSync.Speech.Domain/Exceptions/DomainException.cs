using System;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class DomainException : Exception
    {
        internal DomainException(string message) : base(message)
        {
        }
    }
}