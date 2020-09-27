using LogCorner.EduSync.SignalR.Common;
using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using LogCorner.EduSync.Speech.SharedKernel.Serialyser;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class EventSourcingHandler<T> : IEventSourcingHandler<Event> where T : AggregateRoot<Guid>
    {
        private readonly IEventStoreRepository<T> _eventStoreRepository;
        private readonly IEventSerializer _eventSerializer;
        private readonly ISignalRPublisher _publisher;

        public EventSourcingHandler(IEventStoreRepository<T> eventStoreRepository,
            IEventSerializer eventSerializer, ISignalRPublisher publisher)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventSerializer = eventSerializer;
            _publisher = publisher;
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

            await _publisher.PublishAsync(Topics.Speech, eventStore);
        }
    }
}