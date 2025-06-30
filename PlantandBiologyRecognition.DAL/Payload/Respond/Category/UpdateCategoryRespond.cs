using System;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.Category
{
    public class UpdateCategoryRespond
    {
        public Guid CategoryId { get; set; }
        public string UpdatedName { get; set; }
    }
}