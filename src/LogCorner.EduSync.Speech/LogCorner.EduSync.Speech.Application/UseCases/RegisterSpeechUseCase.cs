using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Domain;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class RegisterSpeechUseCase : IRegisterSpeechUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpeechRepository _speechRepository;
        private readonly IEventSourcingSubscriber _domainEventSubscriber;

        public RegisterSpeechUseCase(IUnitOfWork unitOfWork, ISpeechRepository speechRepository, IEventSourcingSubscriber domainEventSubscriber)
        {
            _unitOfWork = unitOfWork;
            _speechRepository = speechRepository;
            _domainEventSubscriber = domainEventSubscriber;
        }

        public async Task Handle(RegisterSpeechCommandMessage command)
        {
            if (command == null)
            {
                throw new ApplicationArgumentNullException(nameof(command));
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
    }
}