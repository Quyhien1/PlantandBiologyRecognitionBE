using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.Sample
{
    public class CreateSampleRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ScientificName { get; set; }

        public Guid? CategoryId { get; set; }
    }
} 