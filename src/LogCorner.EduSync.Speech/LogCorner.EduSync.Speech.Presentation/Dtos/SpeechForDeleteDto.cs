using System;

namespace LogCorner.EduSync.Speech.Presentation.Dtos
{
    public class SpeechForDeleteDto
    {
        [NotEmpty]
        // [Required(ErrorMessage = "Please provide an Identifier")]
        public Guid Id { get; set; }

        public long Version { get; set; }
    }
}