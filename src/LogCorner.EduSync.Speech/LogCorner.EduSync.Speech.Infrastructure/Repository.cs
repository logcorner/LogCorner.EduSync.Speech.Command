using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class Repository<T, TIdentifier> : IRepository<T, TIdentifier>
        where T : AggregateRoot<TIdentifier>
    {
        private readonly DbSet<T> _dbSet;

        public Repository(DataBaseContext databaseContext)
        {
            _dbSet = databaseContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
    }
}