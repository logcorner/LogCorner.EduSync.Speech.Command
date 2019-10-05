using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public interface IDomainEventRebuilder
    {
        IEnumerable<Event> RebuildDomainEvents(IEnumerable<EventStore> eventStoreItems);
    }
}