namespace PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage
{
    public class UpdateSampleImageRespond
    {
        public Guid ImageId { get; set; }
        public Guid SampleId { get; set; }
        public string SampleName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
} 