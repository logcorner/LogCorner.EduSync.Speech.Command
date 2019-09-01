using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.IRepository
{
    public interface IRepository<T, TIdentifier> where T : AggregateRoot<TIdentifier>
    {
        Task CreateAsync(T entity);
    }
}