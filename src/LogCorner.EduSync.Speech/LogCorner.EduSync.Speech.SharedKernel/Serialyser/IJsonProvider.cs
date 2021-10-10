namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public interface IJsonProvider
    {
        T DeserializeObject<T>(string serializedEvent);

        TEvent DeserializeObject<TEvent>(string serializedEvent, string eventType);

        string SerializeObject<TEvent>(TEvent domainEvent);
    }
}