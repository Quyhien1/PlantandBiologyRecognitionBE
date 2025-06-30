using System;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.LearningTip
{
    public class UpdateLearningTipRespond
    {
        public Guid TipId { get; set; }
        public Guid SampleId { get; set; }
        public string UpdatedTipText { get; set; }
    }
}