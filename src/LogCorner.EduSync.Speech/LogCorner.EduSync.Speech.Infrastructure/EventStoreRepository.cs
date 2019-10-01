using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class EventStoreRepository<T> : IEventStoreRepository<T> where T : AggregateRoot<Guid>
    {
        private readonly IInvoker<T> _invoker;
        private readonly DbSet<EventStore> _dbSet;
        private readonly IEventSerializer _eventSerializer;

        public EventStoreRepository(DataBaseContext databaseContext
            ,
            IInvoker<T> invoker, IEventSerializer eventSerializer)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException(nameof(databaseContext));
            }

            _invoker = invoker;
            _eventSerializer = eventSerializer;

            _dbSet = databaseContext.Set<EventStore>();
        }

        public async Task AppendAsync(EventStore @event)
        {
            await _dbSet.AddAsync(@event);
        }

        public async Task<T> GetByIdAsync<T>(Guid aggregateId)
        {
            T result = default;
            if (aggregateId == Guid.Empty)
            {
                throw new BadAggregateIdException(nameof(aggregateId));
            }

            var aggregate = _invoker.CreateInstanceOfAggregateRoot();
            if (aggregate == null)
            {
                throw new NullInstanceOfAggregateIdException(nameof(aggregate));
            }

            var eventStoreItems = _dbSet.AsNoTracking().Where(e => e.AggregateId == aggregateId).AsQueryable();

            if (!eventStoreItems.Any())
            {
                return await Task.FromResult(result);
            }
            var events = eventStoreItems.Select(@event => _eventSerializer.Deserialize<Event>(@event.SerializedBody, @event.TypeName)).AsEnumerable();
            throw new NotImplementedException();
        }
    }
}