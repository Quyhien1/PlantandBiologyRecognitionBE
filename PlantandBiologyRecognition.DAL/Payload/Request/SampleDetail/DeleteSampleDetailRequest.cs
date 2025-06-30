using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SampleDetail
{
    public class DeleteSampleDetailRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
} 