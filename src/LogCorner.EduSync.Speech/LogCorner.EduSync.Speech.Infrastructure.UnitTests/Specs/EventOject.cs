using System;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class EventOject : Event
    {
        public string FullName { get; }
        public string Adresse { get; }

        public EventOject(Guid aggregateId, string fullName, string adresse)
        {
            AggregateId = aggregateId;
            FullName = fullName;
            Adresse = adresse;
        }
    }
}