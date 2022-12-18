using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.Interfaces
{
    public interface IEventSourcingHandler<in T> where T : IDomainEvent
    {
        Task Handle(T @event, long aggregateVersion);
    }
}