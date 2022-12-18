using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public interface IDomainEventRebuilder
    {
        IEnumerable<Event> RebuildDomainEvents(IEnumerable<EventStore> eventStoreItems);
    }
}