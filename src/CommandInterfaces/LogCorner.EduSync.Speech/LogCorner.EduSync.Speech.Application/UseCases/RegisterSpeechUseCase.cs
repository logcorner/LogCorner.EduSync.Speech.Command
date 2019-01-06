using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class RegisterSpeechUseCase : IRegisterSpeechUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpeechRepository _speechRepository;

        public RegisterSpeechUseCase(IUnitOfWork unitOfWork, ISpeechRepository speechRepository)
        {
            _unitOfWork = unitOfWork;
            _speechRepository = speechRepository;
        }

        public async Task Handle(RegisterSpeechCommandMessage command)
        {
            if (command == null)
            {
                throw new ApplicationArgumentNullException(nameof(command));
            }

            var title = command.Title;
            var urlValue = command.Url;
            var description = command.Description;
            var type = command.Type;

            var speech = new Domain.SpeechAggregate.Speech(title, urlValue, description, type);
            await _speechRepository.CreateAsync(speech);
            _unitOfWork.Commit();
        }
    }
}