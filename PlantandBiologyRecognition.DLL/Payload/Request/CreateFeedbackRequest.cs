using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request
{
    public class CreateFeedbackRequest
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}
