namespace LogCorner.EduSync.Speech.Infrastructure
{
    public interface IJsonProvider
    {
        TEvent DeserializeObject<TEvent>(string serializedEvent, string eventType);
    }
}