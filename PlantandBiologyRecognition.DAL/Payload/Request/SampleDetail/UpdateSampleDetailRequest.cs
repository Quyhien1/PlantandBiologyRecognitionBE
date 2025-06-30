using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SampleDetail
{
    public class UpdateSampleDetailRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid SampleId { get; set; }

        public string? Description { get; set; }

        public string? Habitat { get; set; }

        public string? Behavior { get; set; }

        public string? OtherInfo { get; set; }
    }
} 