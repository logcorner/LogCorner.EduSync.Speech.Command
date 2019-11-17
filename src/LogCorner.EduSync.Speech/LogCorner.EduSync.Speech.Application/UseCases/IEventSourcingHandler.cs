using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public interface IEventSourcingHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event, long aggregateVersion);
    }
}