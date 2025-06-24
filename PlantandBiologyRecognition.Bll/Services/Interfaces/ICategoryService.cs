using System;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Category;
using PlantandBiologyRecognition.DAL.Payload.Respond.Category;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CreateCategoryRespond> CreateCategory(CreateCategoryRequest createCategoryRequest);
        Task<GetCategoryRespond> GetCategoryById(Guid categoryId);
        Task<IPaginate<GetCategoryRespond>> GetAllCategories(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateCategoryRespond> UpdateCategory(UpdateCategoryRequest updateCategoryRequest);
        Task<DeleteCategoryRespond> DeleteCategory(DeleteCategoryRequest deleteCategoryRequest);
    }
} 