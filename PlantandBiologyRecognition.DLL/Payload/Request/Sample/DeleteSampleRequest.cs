using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.Sample
{
    public class DeleteSampleRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
} 