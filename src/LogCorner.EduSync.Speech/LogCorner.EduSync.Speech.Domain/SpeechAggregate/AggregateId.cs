using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class AggregateId
    {
        public static Guid NewId()
        {
            return Guid.NewGuid();
        }
    }
}