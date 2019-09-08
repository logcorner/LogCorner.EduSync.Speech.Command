using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;

namespace LogCorner.EduSync.Speech.Domain.UnitTest
{
    public class StubEventSourcing : AggregateRoot<Guid>
    {
        public void ExposeAddDomainEvent(IDomainEvent stubEvent, long originalVersion)
        {
            AddDomainEvent(stubEvent, originalVersion);
        }
    }
}