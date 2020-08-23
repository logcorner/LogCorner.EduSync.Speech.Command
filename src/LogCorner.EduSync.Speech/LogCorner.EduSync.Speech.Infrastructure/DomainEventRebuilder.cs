using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Collections.Generic;
using System.Linq;
using LogCorner.EduSync.Speech.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class DomainEventRebuilder : IDomainEventRebuilder
    {
        private readonly IEventSerializer _eventSerializer;

        public DomainEventRebuilder(IEventSerializer eventSerializer)
        {
            _eventSerializer = eventSerializer;
        }

        public IEnumerable<Event> RebuildDomainEvents(IEnumerable<EventStore> eventStoreItems)
        {
            var events = eventStoreItems.Select(@event => _eventSerializer.Deserialize<Event>(@event.TypeName, @event.PayLoad)).AsEnumerable();
            return events;
        }
    }
}