using LogCorner.EduSync.Speech.Application.Commands;
using LogCorner.EduSync.Speech.Application.Interfaces;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public interface IDeleteSpeechUseCase : ICommandHandler<DeleteSpeechCommandMessage>
    {
    }
}