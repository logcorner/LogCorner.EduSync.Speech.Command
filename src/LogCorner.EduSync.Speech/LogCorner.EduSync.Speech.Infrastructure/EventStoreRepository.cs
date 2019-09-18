using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class EventStoreRepository<T> : IEventStoreRepository<T> where T : AggregateRoot<Guid>
    {
        private readonly IInvoker<T> _invoker;
        private readonly DbSet<EventStore> _dbSet;

        public EventStoreRepository(DataBaseContext databaseContext
            ,
            IInvoker<T> invoker
            )
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException(nameof(databaseContext));
            }

            _invoker = invoker;

            _dbSet = databaseContext.Set<EventStore>();
        }

        public async Task AppendAsync(EventStore @event)
        {
            await _dbSet.AddAsync(@event);
        }

        public async Task<T> GetByIdAsync<T>(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
            {
                throw new BadAggregateIdException(nameof(aggregateId));
            }

            var aggregate = _invoker.CreateInstanceOfAggregateRoot();
            if (aggregate == null)
            {
                throw new NullInstanceOfAggregateIdException(nameof(aggregate));
            }
            throw new NotImplementedException();
        }
    }
}