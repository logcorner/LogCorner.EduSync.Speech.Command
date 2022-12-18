using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using System.Collections.Generic;
using System.Linq;

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
            var events = eventStoreItems.Select(@event => _eventSerializer.DeserializeEvent<Event>(@event.PayLoad, @event.TypeName)).AsEnumerable();
            return events;
        }
    }
}