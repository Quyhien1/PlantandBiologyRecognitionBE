namespace PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample
{
    public class CreateSavedSampleRespond
    {
        public Guid SavedId { get; set; }
        public Guid UserId { get; set; }
        public Guid SampleId { get; set; }
        public DateTime SavedAt { get; set; }
    }
} 