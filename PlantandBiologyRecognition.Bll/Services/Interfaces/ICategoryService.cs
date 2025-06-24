using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Payload.Request.Category;
using PlantandBiologyRecognition.DAL.Payload.Respond.Category;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CreateCategoryRespond> CreateCategory(CreateCategoryRequest createCategoryRequest);
        Task<GetCategoryRespond> GetCategoryById(Guid categoryId);
        Task<List<GetCategoryRespond>> GetAllCategories();
        Task<UpdateCategoryRespond> UpdateCategory(UpdateCategoryRequest updateCategoryRequest);
        Task<DeleteCategoryRespond> DeleteCategory(DeleteCategoryRequest deleteCategoryRequest);
    }
} 