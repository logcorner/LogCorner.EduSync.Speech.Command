using System;
using LogCorner.EduSync.Speech.Application.Interfaces;

namespace LogCorner.EduSync.Speech.Application.Commands
{
    public class DeleteSpeechCommandMessage : ICommand
    {
        public Guid SpeechId { get; }

        public long OriginalVersion { get; }

        public DeleteSpeechCommandMessage(Guid id, long originalVersion)
        {
            SpeechId = id;
            OriginalVersion = originalVersion;
        }
    }
}