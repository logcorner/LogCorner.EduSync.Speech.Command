using System;

namespace LogCorner.EduSync.Speech.Presentation.Dtos
{
    public class SpeechForDeleteDto
    {
        [NotEmpty]
        public Guid Id { get; set; }

        public long Version { get; set; }
    }
}