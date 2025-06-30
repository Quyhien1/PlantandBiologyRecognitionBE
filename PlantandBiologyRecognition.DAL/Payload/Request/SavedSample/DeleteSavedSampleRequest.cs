using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.SavedSample
{
    public class DeleteSavedSampleRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
} 