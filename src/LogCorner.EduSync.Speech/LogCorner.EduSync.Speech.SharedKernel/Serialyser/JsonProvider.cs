using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public class JsonProvider : IJsonProvider
    {
        public TEvent DeserializeObject<TEvent>(string serializedEvent, string eventType)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            { ContractResolver = new PrivateSetterContractResolver() };
            return (TEvent)JsonConvert.DeserializeObject(serializedEvent, Type.GetType(eventType), settings);
        }

        public string SerializeObject<TEvent>(TEvent domainEvent)
        {
            string serializedEvent = JsonConvert.SerializeObject(domainEvent, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return serializedEvent;
        }

        private class PrivateSetterContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(
                MemberInfo member,
                MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);
                if (!prop.Writable)
                {
                    var property = member as PropertyInfo;
                    if (property != null)
                    {
                        var hasPrivateSetter = property.GetSetMethod(true) != null;
                        prop.Writable = hasPrivateSetter;
                    }
                }
                return prop;
            }
        }
    }
}