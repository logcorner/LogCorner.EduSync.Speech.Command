using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser
{
    public interface IEventSerializer
    {
        TEvent DeserializeEvent<TEvent>(string serializedEvent, string eventType) where TEvent : IDomainEvent;

        string SerializeEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }
}