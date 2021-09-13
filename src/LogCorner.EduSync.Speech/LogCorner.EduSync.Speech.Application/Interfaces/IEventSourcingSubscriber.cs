using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.Interfaces
{
    public interface IEventSourcingSubscriber
    {
        Task Subscribe(IEventSourcing aggregate);
    }
}