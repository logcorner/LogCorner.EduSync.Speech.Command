using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public interface IEventSourcingSubscriber
    {
        Task Subscribe(IEventSourcing aggregate);
    }
}