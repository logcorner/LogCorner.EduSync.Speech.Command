namespace LogCorner.EduSync.Speech.SharedKernel.Serialyser
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T objectValue);
        T Deserialize<T>(string stringValue);
    }
}