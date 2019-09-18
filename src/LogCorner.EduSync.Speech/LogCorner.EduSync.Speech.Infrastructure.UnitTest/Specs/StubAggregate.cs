using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class StubAggregate : AggregateRoot<Guid>
    {
        private StubAggregate()
        {
        }
    }
}