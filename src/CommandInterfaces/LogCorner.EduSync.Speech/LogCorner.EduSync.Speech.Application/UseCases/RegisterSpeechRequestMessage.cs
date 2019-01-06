using LogCorner.EduSync.Speech.Application.Interfaces;

namespace LogCorner.EduSync.Speech.Application.UseCases
{
    public class RegisterSpeechCommandMessage : ICommand
    {
        public string Title { get; }
        public string Description { get; }
        public string Url { get; }
        public string Type { get; }

        public RegisterSpeechCommandMessage(string title, string description, string url, string type)
        {
            Title = title;
            Description = description;
            Url = url;
            Type = type;
        }
    }
}