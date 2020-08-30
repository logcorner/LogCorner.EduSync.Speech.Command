using LogCorner.EduSync.Speech.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public class JsonEventSerializer : IEventSerializer
    {
        private readonly IJsonProvider _jsonProvider;

        public JsonEventSerializer(IJsonProvider jsonProvider)
        {
            _jsonProvider = jsonProvider;
        }

        public TEvent Deserialize<TEvent>(string eventType, string serializedEvent) where TEvent : IDomainEvent
        {
            return _jsonProvider.DeserializeObject<TEvent>(serializedEvent, eventType);
        }

        public string Serialize<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            return _jsonProvider.SerializeObject<TEvent>(domainEvent);
        }
    }
}