using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface ISpeechRepository
    {
        Task CreateAsync(Speech entity);
    }
}