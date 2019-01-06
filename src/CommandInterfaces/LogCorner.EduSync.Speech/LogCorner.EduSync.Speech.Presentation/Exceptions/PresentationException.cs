using System;

namespace LogCorner.EduSync.Speech.Presentation.Exceptions
{
    public class PresentationException : Exception
    {
        internal PresentationException(string message) : base(message)
        {
        }
    }
}