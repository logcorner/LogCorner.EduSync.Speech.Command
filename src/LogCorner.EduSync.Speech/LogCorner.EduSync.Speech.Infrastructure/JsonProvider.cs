using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class JsonProvider : IJsonProvider
    {
        public TEvent DeserializeObject<TEvent>(string serializedEvent, string eventType)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            { ContractResolver = new PrivateSetterContractResolver() };
            return (TEvent)JsonConvert.DeserializeObject(serializedEvent, Type.GetType(eventType), settings);
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