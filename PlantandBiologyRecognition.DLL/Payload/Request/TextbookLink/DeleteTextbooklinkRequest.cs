using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request.TextBooLink
{
    public class DeleteTextbooklinkRequest
    {
        public Guid LinkId { get; set; }
        public string Reason { get; set; }
    }
}
