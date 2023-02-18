using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using LogCorner.EduSync.Speech.Resiliency;
using LogCorner.EduSync.Speech.Telemetry;
using OpenTelemetry.Context.Propagation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.EventSourcing
{
    public class EventSourcingHandler : IEventSourcingHandler<Event>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IEventSerializer _eventSerializer;
        private readonly IJsonSerializer _serializer;
        private readonly IEventPublisher _eventPublisher;

        private readonly ITraceService _traceService;
        private readonly IResiliencyService _resiliencyService;
        private static readonly ActivitySource Activity = new("command-api");

        private static readonly TextMapPropagator Propagator = Propagators.DefaultTextMapPropagator;

        public EventSourcingHandler(IUnitOfWork unitOfWork, IEventStoreRepository eventStoreRepository,
            IEventSerializer eventSerializer, IEventPublisher eventPublisher, IJsonSerializer serializer, ITraceService traceService, IResiliencyService resiliencyService)
        {
            _unitOfWork = unitOfWork;
            _eventStoreRepository = eventStoreRepository;
            _eventSerializer = eventSerializer;
            _eventPublisher = eventPublisher;
            _serializer = serializer;
            _traceService = traceService;
            _resiliencyService = resiliencyService;
        }

        public async Task Handle(Event @event, long aggregateVersion)
        {
            using var activity = Activity.StartActivity($"Publishing Event AggregateId : {@event.AggregateId} , AggregateVersion : {@event.AggregateVersion} ", ActivityKind.Producer);
            if (@event == null)
            {
                throw new EventNullException(nameof(@event));
            }

            IDictionary<string, string> headers = new Dictionary<string, string>();
            _traceService.AddActivityToHeader(activity, headers, Propagator);

            var serializedBody = _eventSerializer.SerializeEvent(@event);

            var eventStore = new EventStore(@event.AggregateId, aggregateVersion,
                $"{aggregateVersion}@{@event.AggregateId}",
                @event.GetType().AssemblyQualifiedName,
                @event.OcurrendOn,
                serializedBody);
            await _eventStoreRepository.AppendAsync(eventStore);
            _unitOfWork.Commit();

            var jsonString = _serializer.Serialize(eventStore);

            var tags = new Dictionary<string, object>
            {
                {"@event.AggregateId", @event.AggregateId },
                {"@event.AggregateVersion", @event.AggregateVersion} ,
                {"@event.EventId", @event.EventId},
                {"@event.Payload", serializedBody}
            };
            _traceService.SetActivityTags(activity, tags);
           //////////////////// await _resiliencyService.ExponentialExceptionRetry.ExecuteAsync(async () => await _eventPublisher.PublishAsync(Topics.Speech, jsonString));
        }
    }
}