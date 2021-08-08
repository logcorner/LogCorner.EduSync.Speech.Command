using LogCorner.EduSync.Speech.Application.Commands;
using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Domain;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class SpeechUseCase : ICreateSpeechUseCase,
                                 IUpdateSpeechUseCase,
                                 IDeleteSpeechUseCase
    {
        private readonly ISpeechRepository _speechRepository;
        private readonly IEventSourcingSubscriber _domainEventSubscriber;
        private readonly IEventStoreRepository _eventStoreRepository;

        public SpeechUseCase(ISpeechRepository speechRepository, IEventSourcingSubscriber domainEventSubscriber, IEventStoreRepository eventStoreRepository)
        {
            _speechRepository = speechRepository;
            _domainEventSubscriber = domainEventSubscriber;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task Handle(RegisterSpeechCommandMessage command)
        {
            if (command == null)
            {
                throw new ArgumentNullApplicationException(nameof(command));
            }

            var title = new Title(command.Title);
            var urlValue = new UrlValue(command.Url);
            var description = new Description(command.Description);
            var type = new SpeechType(command.Type);

            var speech = new Domain.SpeechAggregate.Speech(AggregateId.NewId(), title, urlValue, description, type);
            await _speechRepository.CreateAsync(speech);
            await _domainEventSubscriber.Subscribe(speech);
        }

        public async Task Handle(UpdateSpeechCommandMessage command)
        {
            if (command == null)
            {
                throw new ArgumentNullApplicationException(nameof(command));
            }

            var speech = await _eventStoreRepository.GetByIdAsync<Domain.SpeechAggregate.Speech>(command.SpeechId);

            if (speech == null || speech.Id == Guid.Empty)
            {
                throw new NotFoundApplicationException($"speech not found {command.SpeechId}");
            }
            long committedVersion = command.OriginalVersion;
            if (command.Title != null && speech.Title.Value != command.Title)
            {
                speech.ChangeTitle(new Title(command.Title), committedVersion++);
            }
            if (command.Description != null && speech.Description.Value != command.Description)
            {
                speech.ChangeDescription(new Description(command.Description), committedVersion++);
            }
            if (command.Url != null && speech.Url.Value != command.Url)
            {
                speech.ChangeUrl(new UrlValue(command.Url), committedVersion++);
            }

            if (command.Type != null && speech.Type != new SpeechType(command.Type.Value))
            {
                speech.ChangeType(new SpeechType(command.Type.Value), committedVersion);
            }

            await _speechRepository.UpdateAsync(speech);
            await _domainEventSubscriber.Subscribe(speech);
        }

        public async Task Handle(DeleteSpeechCommandMessage command)
        {
            if (command == null)
            {
                throw new ArgumentNullApplicationException(nameof(command));
            }
            var speech = await _eventStoreRepository.GetByIdAsync<Domain.SpeechAggregate.Speech>(command.SpeechId);
            if (speech == null)
            {
                throw new NotFoundApplicationException($"speech not found {command.SpeechId}");
            }

            speech.Delete(command.SpeechId, command.OriginalVersion);

            await _speechRepository.DeleteAsync(speech);
            await _domainEventSubscriber.Subscribe(speech);
        }
    }
}