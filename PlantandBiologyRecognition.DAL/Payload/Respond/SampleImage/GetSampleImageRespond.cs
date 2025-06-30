using System;
namespace PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage
{
    public class GetSampleImageRespond
    {
        public Guid ImageId { get; set; }
        public Guid SampleId { get; set; }
        public string SampleName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
} 