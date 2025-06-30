using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SampleImage
{
    public class UpdateSampleImageRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid SampleId { get; set; }
        public string? ImageUrl { get; set; }

        public string? Description { get; set; }
    }
} 