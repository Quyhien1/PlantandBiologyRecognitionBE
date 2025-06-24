using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request.TextBooLink
{
    public class CreateTextbooklinkRequest
    {
        public Guid? SampleId { get; set; }
        public string TextbookName { get; set; }
        public int? PageNumber { get; set; }
        public string ContentSummary { get; set; }
    }
}
