namespace LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string stringValue);

        T Deserialize<T>(string type, string stringValue);

        string Serialize<T>(T objectValue);
    }
}