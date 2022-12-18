using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class StubAggregate : AggregateRoot<Guid>
    {
        public string FullName { get; private set; }
        public string Adresse { get; private set; }

        private StubAggregate()
        {
        }

        public void Apply(EventOject eventOject)
        {
            Id = eventOject.AggregateId;
            FullName = eventOject.FullName;
            Adresse = eventOject.Adresse;
        }
    }
}