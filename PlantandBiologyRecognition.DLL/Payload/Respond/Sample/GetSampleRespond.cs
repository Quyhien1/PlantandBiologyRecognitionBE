namespace PlantandBiologyRecognition.DAL.Payload.Respond.Sample
{
    public class GetSampleRespond
    {
        public Guid SampleId { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
} 