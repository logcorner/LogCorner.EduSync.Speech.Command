using System.Collections.Generic;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public interface IDomainEventRebuilder
    {
        IEnumerable<Event> RebuildDomainEvents(IEnumerable<EventStore> eventStoreItems);
    }
}