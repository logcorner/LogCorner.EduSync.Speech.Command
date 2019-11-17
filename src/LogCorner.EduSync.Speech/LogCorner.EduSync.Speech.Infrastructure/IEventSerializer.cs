using LogCorner.EduSync.Speech.Domain.SpeechAggregate;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public interface IEventSerializer
    {
        TEvent Deserialize<TEvent>(string serializedEvent, string eventType) where TEvent : IDomainEvent;

        string Serialize<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}