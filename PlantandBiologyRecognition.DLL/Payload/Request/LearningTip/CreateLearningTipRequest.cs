using System;
using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.LearningTip
{
    public class CreateLearningTipRequest
    {
        [Required]
        public Guid SampleId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string TipText { get; set; }
    }
}