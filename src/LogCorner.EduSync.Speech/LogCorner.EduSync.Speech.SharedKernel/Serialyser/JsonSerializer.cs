namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public class JsonSerializer : IJsonSerializer
    {
        private readonly IJsonProvider _jsonProvider;

        public JsonSerializer(IJsonProvider jsonProvider)
        {
            _jsonProvider = jsonProvider;
          
        }
        public string Serialize<T>(T objectValue)
        {
            return _jsonProvider.SerializeObject<T>(objectValue);
        }

        public T Deserialize<T>(string stringValue)
        {
            return _jsonProvider.DeserializeObject<T>(stringValue);
        }
    }
}