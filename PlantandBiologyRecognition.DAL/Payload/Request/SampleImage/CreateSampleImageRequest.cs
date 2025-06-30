using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SampleImage
{
    public class CreateSampleImageRequest
    {
        [Required]
        public Guid SampleId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
} 