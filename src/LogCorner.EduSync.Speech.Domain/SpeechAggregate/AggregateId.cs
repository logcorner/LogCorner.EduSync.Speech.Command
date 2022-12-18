using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public static class AggregateId
    {
        public static Guid NewId()
        {
            return Guid.NewGuid();
        }
    }
}