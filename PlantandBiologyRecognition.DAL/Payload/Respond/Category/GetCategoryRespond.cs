using System;

namespace PlantandBiologyRecognition.DAL.Payload.Respond.Category
{
    public class GetCategoryRespond
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}