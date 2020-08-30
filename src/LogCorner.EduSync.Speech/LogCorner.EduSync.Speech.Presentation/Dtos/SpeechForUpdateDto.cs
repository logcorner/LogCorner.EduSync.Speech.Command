using System;
using System.ComponentModel.DataAnnotations;

namespace LogCorner.EduSync.Speech.Presentation.Dtos
{
    public class SpeechForUpdateDto
    {
        [Required(ErrorMessage = "Please provide an Identifier")]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Please provide a Title")]
        [StringLength(60, ErrorMessage = "The Title length must be between 10 and 60 characters",
            MinimumLength = 10)]
        public string Title { get; set; }

        //[Required(ErrorMessage = "Please provide a Description")]
        [StringLength(500, ErrorMessage = "The Description length must be between 100 and 500 characters",
            MinimumLength = 100)]
        public string Description { get; set; }

       // [Required(ErrorMessage = "Please provide a valid Url")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

      // [Required(ErrorMessage = "Please provide a Type")]
        public string Type { get; set; }

        public long Version { get; set; }
    }
}