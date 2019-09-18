using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventStoreRepository<T> where T : AggregateRoot<Guid>
    {
        Task<T> GetByIdAsync<T>(Guid aggregateId);

        Task AppendAsync(EventStore @event);
    }
}