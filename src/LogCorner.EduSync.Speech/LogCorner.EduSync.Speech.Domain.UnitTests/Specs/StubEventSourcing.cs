using System;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;

namespace LogCorner.EduSync.Speech.Domain.UnitTests.Specs
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