namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public interface IJsonProvider
    {
        TEvent DeserializeObject<TEvent>(string serializedEvent, string eventType);

        string SerializeObject<TEvent>(TEvent domainEvent);
    }
}