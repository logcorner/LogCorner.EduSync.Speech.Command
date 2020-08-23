using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;
using LogCorner.EduSync.Speech.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Domain.UnitTest.Specs
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