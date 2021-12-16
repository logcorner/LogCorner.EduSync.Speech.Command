using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser
{
    public interface IEventSerializer
    {
        TEvent Deserialize<TEvent>(string serializedEvent, string eventType) where TEvent : IDomainEvent;

        string Serialize<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}