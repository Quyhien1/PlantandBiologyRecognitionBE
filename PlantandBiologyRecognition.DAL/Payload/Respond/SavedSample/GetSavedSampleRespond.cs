namespace PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample
{
    public class GetSavedSampleRespond
    {
        public Guid SavedId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid SampleId { get; set; }
        public string SampleName { get; set; }
        public DateTime SavedAt { get; set; }
    }
} 