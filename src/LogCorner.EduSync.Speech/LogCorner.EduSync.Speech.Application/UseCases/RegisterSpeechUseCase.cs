using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Domain;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class RegisterSpeechUseCase : IRegisterSpeechUseCase, IUpdateSpeechUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpeechRepository _speechRepository;
        private readonly IEventSourcingSubscriber _domainEventSubscriber;
        private readonly IEventStoreRepository<Domain.SpeechAggregate.Speech> _eventStoreRepository;

        public RegisterSpeechUseCase(IUnitOfWork unitOfWork, ISpeechRepository speechRepository, IEventSourcingSubscriber domainEventSubscriber, IEventStoreRepository<Domain.SpeechAggregate.Speech> eventStoreRepository)
        {
            _unitOfWork = unitOfWork;
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
            _unitOfWork.Commit();
        }

        public async Task Handle(UpdateSpeechCommandMessage command)
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

            if (speech.Title.Value != command.Title)
            {
                speech.ChangeTitle(command.Title, command.OriginalVersion);
            }
            await _speechRepository.UpdateAsync(speech);
            await _domainEventSubscriber.Subscribe(speech);
            _unitOfWork.Commit();
        }
    }
}