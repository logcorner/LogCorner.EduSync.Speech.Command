using LogCorner.EduSync.Speech.Application.Interfaces;

namespace LogCorner.EduSync.Speech.Application.Commands
{
    public class RegisterSpeechCommandMessage : ICommand
    {
        public string Title { get; }
        public string Description { get; }
        public string Url { get; }
        public int Type { get; }

        public RegisterSpeechCommandMessage(string title, string description, string url, int type)
        {
            Title = title;
            Description = description;
            Url = url;
            Type = type;
        }
    }
}