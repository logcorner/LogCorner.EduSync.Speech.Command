using LogCorner.EduSync.Speech.Domain.SpeechAggregate;

namespace LogCorner.EduSync.Speech.Infrastructure
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
    }
}