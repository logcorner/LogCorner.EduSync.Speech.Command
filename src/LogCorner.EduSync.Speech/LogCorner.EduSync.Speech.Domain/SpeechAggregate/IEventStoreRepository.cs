using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventStoreRepository
    {
        Task<T> GetByIdAsync<T>(Guid aggregateId) where T : AggregateRoot<Guid>;

        Task AppendAsync(EventStore @event);
    }
}