using System;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.LearningTip
{
    public class CreateLearningTipRespond
    {
        public Guid TipId { get; set; }
        public Guid SampleId { get; set; }
        public string TipText { get; set; }
    }
}