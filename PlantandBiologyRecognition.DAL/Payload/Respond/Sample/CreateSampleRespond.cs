namespace PlantandBiologyRecognition.DAL.Payload.Respond.Sample
{
    public class CreateSampleRespond
    {
        public Guid SampleId { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public Guid? CategoryId { get; set; }
    }
}