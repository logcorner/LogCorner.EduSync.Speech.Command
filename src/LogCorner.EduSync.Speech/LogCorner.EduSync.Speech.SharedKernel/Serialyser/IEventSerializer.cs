using LogCorner.EduSync.Speech.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public interface IEventSerializer
    {
        TEvent Deserialize<TEvent>(string serializedEvent, string eventType) where TEvent : IDomainEvent;

        string Serialize<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}