using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Respond
{
    public class CreateFeedbackRespond
    {
        public Guid FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public String Message { get; set; }
        public DateOnly SubmittedAt { get; set; }
    }
}
