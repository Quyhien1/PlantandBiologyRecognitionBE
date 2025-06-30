using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.TextbookLink
{
    public class GetTextbooklinkRespond
    {
        public Guid LinkId { get; set; }
        public Guid? SampleId { get; set; }
        public string TextbookName { get; set; }
        public int? PageNumber { get; set; }
        public string ContentSummary { get; set; }
    }
}
