using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.DAL.Payload.Request.TextbookLink
{
    public class UpdateTextbooklinkRequest
    {
        public Guid LinkId { get; set; }
        public Guid? SampleId { get; set; }
        public string TextbookName { get; set; }
        public int? Chapter { get; set; }
        public string Lesson { get; set; }
    }
}
