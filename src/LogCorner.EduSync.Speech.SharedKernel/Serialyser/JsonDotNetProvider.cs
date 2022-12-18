using System;
using System.Text.Json;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser
{
    public class JsonDotNetProvider : IJsonProvider
    {
        public T DeserializeObject<T>(string serializedEvent)
        {
            JsonSerializerOptions settings = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            var deserializeObject = JsonSerializer.Deserialize<T>(serializedEvent, settings);
            return deserializeObject;
        }

        public TEvent DeserializeObject<TEvent>(string serializedEvent, string eventType)
        {
            JsonSerializerOptions settings = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            var result = (TEvent)JsonSerializer.Deserialize(serializedEvent, Type.GetType(eventType) ?? throw new Exception("could not get eventType"), settings);
            return result;
        }

        public string SerializeObject<TEvent>(TEvent domainEvent)
        {
            string serializedEvent = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());

            return serializedEvent;
        }
    }
}