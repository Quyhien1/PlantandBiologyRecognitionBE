using System;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Category;
using PlantandBiologyRecognition.DAL.Payload.Respond.Category;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
/// Creates a new category based on the provided request data.
/// </summary>
/// <param name="createCategoryRequest">The request containing details for the new category.</param>
/// <returns>A task that resolves to the response containing the result of the category creation.</returns>
Task<CreateCategoryRespond> CreateCategory(CreateCategoryRequest createCategoryRequest);
        /// <summary>
/// Retrieves a category by its unique identifier.
/// </summary>
/// <param name="categoryId">The unique identifier of the category to retrieve.</param>
/// <returns>A task that represents the asynchronous operation, containing the category details if found.</returns>
Task<GetCategoryRespond> GetCategoryById(Guid categoryId);
        /// <summary>
/// Retrieves a paginated list of categories, optionally filtered by a search term.
/// </summary>
/// <param name="page">The page number to retrieve. Defaults to 1.</param>
/// <param name="size">The number of categories per page. Defaults to 10.</param>
/// <param name="searchTerm">An optional term to filter categories by name or description.</param>
/// <returns>A paginated result containing category response objects.</returns>
Task<IPaginate<GetCategoryRespond>> GetAllCategories(int page = 1, int size = 10, string searchTerm = null);
        /// <summary>
/// Updates an existing category with the provided information.
/// </summary>
/// <param name="updateCategoryRequest">The request containing updated category details.</param>
/// <returns>A task that represents the asynchronous operation, containing the update response.</returns>
Task<UpdateCategoryRespond> UpdateCategory(UpdateCategoryRequest updateCategoryRequest);
        /// <summary>
/// Deletes a category based on the provided request details.
/// </summary>
/// <param name="deleteCategoryRequest">The request containing information about the category to delete.</param>
/// <returns>A task that resolves to a response indicating the result of the deletion operation.</returns>
Task<DeleteCategoryRespond> DeleteCategory(DeleteCategoryRequest deleteCategoryRequest);
    }
} 