using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class EventStoreRepository<T> : IEventStoreRepository where T : AggregateRoot<Guid>
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

        public async Task<TU> GetByIdAsync<TU>(Guid aggregateId) where TU : AggregateRoot<Guid>
        {
            if (aggregateId == Guid.Empty)
            {
                throw new BadAggregateIdException(ErrorCode.BadAggregateId, nameof(aggregateId));
            }

            var aggregate = _invoker.CreateInstanceOfAggregateRoot<TU>();
            if (aggregate == null)
            {
                throw new NullInstanceOfAggregateException(ErrorCode.NullInstanceOfAggregate, nameof(aggregate));
            }

            var eventStoreItems = _dbSet.AsNoTracking().Where(e => e.AggregateId == aggregateId).AsQueryable();

            if (!eventStoreItems.Any())
            {
                return await Task.FromResult<TU>(null);
            }

            var events = _eventStoreToEVent.RebuildDomainEvents(eventStoreItems);
            aggregate.LoadFromHistory(events);

            return await Task.FromResult(aggregate);
        }
    }
}