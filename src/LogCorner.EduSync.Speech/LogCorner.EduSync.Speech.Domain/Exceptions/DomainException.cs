using System;

namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}