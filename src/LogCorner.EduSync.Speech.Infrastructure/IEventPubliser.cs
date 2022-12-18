using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure;

public interface IEventPublisher
{
    Task PublishAsync(string topic, string @event);
}