using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class EventSourcingSubscriber : IEventSourcingSubscriber
    {
        private readonly IEventSourcingHandler<Event> _handler;

        public EventSourcingSubscriber(IEventSourcingHandler<Event> handler)
        {
            _handler = handler;
        }

        public async Task Subscribe(IEventSourcing aggregate)
        {
            foreach (var evt in aggregate.GetUncommittedEvents())
            {
                await _handler.Handle((Event)evt, aggregate.Version);
            }
        }
    }
}