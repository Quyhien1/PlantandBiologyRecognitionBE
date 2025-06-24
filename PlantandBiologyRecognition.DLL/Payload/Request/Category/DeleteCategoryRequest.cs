using System;
using System.ComponentModel.DataAnnotations;

namespace PlantandBiologyRecognition.DAL.Payload.Request.Category
{
    public class DeleteCategoryRequest
    {
        [Required]
        public Guid CategoryId { get; set; }

        [MaxLength(50)]
        public string? Reason { get; set; }
    }
}