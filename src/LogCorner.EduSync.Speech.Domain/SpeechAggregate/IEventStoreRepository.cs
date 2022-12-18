using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface IEventStoreRepository
    {
        Task AppendAsync(EventStore @event);

        Task<TU> GetByIdAsync<TU>(Guid aggregateId) where TU : AggregateRoot<Guid>;
    }
}