using System;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventStoreRepository
    {
        Task AppendAsync(EventStore @event);

        Task<T> GetByIdAsync<T>(Guid aggregateId) where T : AggregateRoot<Guid>;
    }
}