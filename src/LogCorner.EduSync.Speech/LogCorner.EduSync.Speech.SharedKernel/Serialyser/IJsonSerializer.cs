namespace LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T objectValue);

        T Deserialize<T>(string stringValue);

        public T Deserialize<T>(string type, string stringValue);
    }
}