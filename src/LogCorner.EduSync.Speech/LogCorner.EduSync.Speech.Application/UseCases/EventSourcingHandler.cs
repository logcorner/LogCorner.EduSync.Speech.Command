using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class EventSourcingHandler<T> : IEventSourcingHandler<Event> where T : AggregateRoot<Guid>
    {
        private readonly IEventStoreRepository<T> _eventStoreRepository;
        private readonly IEventSerializer _eventSerializer;

        public EventSourcingHandler(IEventStoreRepository<T> eventStoreRepository,
            IEventSerializer eventSerializer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventSerializer = eventSerializer;
        }

        public async Task Handle(Event @event, long aggregateVersion)
        {
            if (@event == null)
            {
                throw new EventNullException(nameof(@event));
            }

            var serializedBody = _eventSerializer.Serialize(@event);

            var eventStore = new EventStore(@event.AggregateId, aggregateVersion,
                $"{aggregateVersion}@{@event.AggregateId}",
                @event.GetType().AssemblyQualifiedName,
                @event.OcurrendOn,
                serializedBody);
            await _eventStoreRepository.AppendAsync(eventStore);
        }
    }
}