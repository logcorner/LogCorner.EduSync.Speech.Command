using System;

namespace LogCorner.EduSync.Speech.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        protected InfrastructureException(string message) : base(message)
        {
        }
    }
}