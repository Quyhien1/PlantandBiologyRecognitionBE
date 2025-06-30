using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SavedSample
{
    public class UpdateSavedSampleRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid SampleId { get; set; }
    }
} 