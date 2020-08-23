using System.Threading.Tasks;
using LogCorner.EduSync.Speech.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public interface IEventSourcingHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event, long aggregateVersion);
    }
}