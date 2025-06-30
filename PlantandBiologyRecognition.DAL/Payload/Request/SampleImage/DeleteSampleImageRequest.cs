using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SampleImage
{
    public class DeleteSampleImageRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
} 