using System;
using System.Runtime.Serialization;

namespace LogCorner.EduSync.Speech.Infrastructure.Exceptions
{
    [Serializable]
    public class InfrastructureException : Exception
    {
        protected InfrastructureException(string message) : base(message)
        {
        }

        protected InfrastructureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}