using System;
using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.LearningTip
{
    public class DeleteLearningTipRequest
    {
        [Required]
        public Guid TipId { get; set; }

        [MaxLength(50)]
        public string? Reason { get; set; }
    }
}