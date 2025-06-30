using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SavedSample
{
    public class CreateSavedSampleRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid SampleId { get; set; }
    }
} 