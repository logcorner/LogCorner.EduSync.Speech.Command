using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Application.Interfaces
{
    public interface IEventSourcingHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event, long aggregateVersion);
    }
}