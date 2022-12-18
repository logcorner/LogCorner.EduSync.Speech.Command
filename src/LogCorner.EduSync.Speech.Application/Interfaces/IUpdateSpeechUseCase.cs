using LogCorner.EduSync.Speech.Application.Commands;

namespace LogCorner.EduSync.Speech.Application.Interfaces
{
    public interface IUpdateSpeechUseCase : ICommandHandler<UpdateSpeechCommandMessage>
    {
    }
}