using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;

namespace LogCorner.EduSync.Speech.Domain.IRepository
{
    public interface IRepository<T, TIdentifier> where T : AggregateRoot<TIdentifier>
    {
        Task<T> GetByIdAsync(TIdentifier id);

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
    }
}