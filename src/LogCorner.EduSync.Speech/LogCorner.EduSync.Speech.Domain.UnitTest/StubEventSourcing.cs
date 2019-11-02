using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;

namespace LogCorner.EduSync.Speech.Domain.UnitTest
{
    public class StubEventSourcing : AggregateRoot<Guid>
    {
        public object Value { get; private set; }

        public void ExposeAddDomainEvent(IDomainEvent stubEvent, long originalVersion)
        {
            AddDomainEvent(stubEvent, originalVersion);
        }

        public void Apply(SubEvent subEvent)
        {
            Value = subEvent.Value;
        }
    }
}