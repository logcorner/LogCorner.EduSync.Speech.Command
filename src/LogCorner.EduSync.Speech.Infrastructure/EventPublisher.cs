using LogCorner.EduSync.Speech.Producer;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure;

public class EventPublisher : IEventPublisher
{
    private readonly IProducerService _producerService;

    public EventPublisher(IProducerService producerService)
    {
        _producerService = producerService;
    }

    public async Task PublishAsync(string topic, string @event)
    {
        await _producerService.ProduceAsync(topic, @event);
    }
}