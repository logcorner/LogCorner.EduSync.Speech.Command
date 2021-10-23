using LogCorner.EduSync.SignalR.Common;
using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using LogCorner.EduSync.Speech.SharedKernel.Serialyser;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class EventSourcingHandler : IEventSourcingHandler<Event>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventSerializer _eventSerializer;
        private readonly ISignalRPublisher _publisher;

        public EventSourcingHandler(IUnitOfWork unitOfWork, IEventStoreRepository eventStoreRepository,
            IEventSerializer eventSerializer, ISignalRPublisher publisher)
        {
            _unitOfWork = unitOfWork;
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
            _unitOfWork.Commit();
             await _publisher.PublishAsync(Topics.Speech, eventStore);
        }
    }
}