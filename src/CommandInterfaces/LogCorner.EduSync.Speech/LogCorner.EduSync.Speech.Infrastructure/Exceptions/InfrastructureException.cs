using System;

namespace LogCorner.EduSync.Speech.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        internal InfrastructureException(string message) : base(message)
        {
        }
    }
}