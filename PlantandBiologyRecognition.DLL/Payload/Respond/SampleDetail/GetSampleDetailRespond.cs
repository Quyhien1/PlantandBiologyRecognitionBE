namespace PlantandBiologyRecognition.DAL.Payload.Respond.SampleDetail
{
    public class GetSampleDetailRespond
    {
        public Guid DetailId { get; set; }
        public Guid SampleId { get; set; }
        public string SampleName { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public string Behavior { get; set; }
        public string OtherInfo { get; set; }
    }
} 