using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.Interfaces
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand message);
    }
}