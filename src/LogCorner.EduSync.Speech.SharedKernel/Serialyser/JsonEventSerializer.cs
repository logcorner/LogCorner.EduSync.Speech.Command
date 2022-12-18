using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser
{
    public class JsonEventSerializer : IEventSerializer, IJsonSerializer
    {
        private readonly IJsonProvider _jsonProvider;

        public JsonEventSerializer(IJsonProvider jsonProvider)
        {
            _jsonProvider = jsonProvider;
        }

        public TEvent DeserializeEvent<TEvent>(string serializedEvent, string eventType) where TEvent : IDomainEvent
        {
            return _jsonProvider.DeserializeObject<TEvent>(serializedEvent, eventType);
        }

        public string SerializeEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            return _jsonProvider.SerializeObject(domainEvent);
        }

        public T Deserialize<T>(string stringValue)
        {
            return _jsonProvider.DeserializeObject<T>(stringValue);
        }

        public T Deserialize<T>(string type, string stringValue)
        {
            return _jsonProvider.DeserializeObject<T>(stringValue, type);
        }

        public string Serialize<T>(T objectValue)
        {
            return _jsonProvider.SerializeObject(objectValue);
        }
    }
}