using LogCorner.EduSync.Speech.SharedKernel.Events;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventStoreRepository
    {
        Task AppendAsync(EventStore @event);

        Task<T> GetByIdAsync<T>(Guid aggregateId) where T : AggregateRoot<Guid>;
    }
}