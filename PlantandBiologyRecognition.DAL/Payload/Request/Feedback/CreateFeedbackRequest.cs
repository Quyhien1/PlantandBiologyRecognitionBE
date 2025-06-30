using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request.Feedback
{
    public class CreateFeedbackRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(1_000)]
        public string Message { get; set; }
    }
}
