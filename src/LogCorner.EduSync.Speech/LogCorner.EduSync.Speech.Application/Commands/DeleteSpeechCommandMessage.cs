using LogCorner.EduSync.Speech.Application.Interfaces;
using System;

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