using LogCorner.EduSync.Speech.Domain.IRepository;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface ISpeechRepository : IRepository<Speech, Guid>
    {
        Task UpdateAsync(Speech speech);
    }
}