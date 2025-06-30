using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request.Feedback
{
    public class DeleteFeedbackRequest
    {
        [Required]
        public Guid FeedbackId { get; set; }
        [MaxLength(50)]
        public string? Reason { get; set; }
    }
}
