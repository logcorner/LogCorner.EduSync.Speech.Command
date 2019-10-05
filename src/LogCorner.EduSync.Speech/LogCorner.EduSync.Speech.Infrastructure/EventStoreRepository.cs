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
        private readonly IDomainEventRebuilder _eventStoreToEVent;

        public EventStoreRepository(DataBaseContext databaseContext,
            IInvoker<T> invoker, IDomainEventRebuilder eventStoreToEVent)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException(nameof(databaseContext));
            }

            _invoker = invoker;
            _eventStoreToEVent = eventStoreToEVent;

            _dbSet = databaseContext.Set<EventStore>();
        }

        public async Task AppendAsync(EventStore @event)
        {
            await _dbSet.AddAsync(@event);
        }

        public async Task<T> GetByIdAsync<T>(Guid aggregateId) where T : AggregateRoot<Guid>
        {
            if (aggregateId == Guid.Empty)
            {
                throw new BadAggregateIdException(nameof(aggregateId));
            }

            var aggregate = _invoker.CreateInstanceOfAggregateRoot<T>();
            if (aggregate == null)
            {
                throw new NullInstanceOfAggregateIdException(nameof(aggregate));
            }

            var eventStoreItems = _dbSet.AsNoTracking().Where(e => e.AggregateId == aggregateId).AsQueryable();

            if (!eventStoreItems.Any())
            {
                return await Task.FromResult(aggregate);
            }

            var events = _eventStoreToEVent.RebuildDomainEvents(eventStoreItems);
            aggregate.LoadFromHistory(events);

            return await Task.FromResult(aggregate);
        }
    }
}
