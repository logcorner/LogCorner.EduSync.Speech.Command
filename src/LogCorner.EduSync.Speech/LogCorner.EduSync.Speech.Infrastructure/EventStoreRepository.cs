using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly DbSet<EventStore> _dbSet;

        public EventStoreRepository(DataBaseContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException(nameof(databaseContext));
            }

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
            throw new NotImplementedException();
        }
    }
}