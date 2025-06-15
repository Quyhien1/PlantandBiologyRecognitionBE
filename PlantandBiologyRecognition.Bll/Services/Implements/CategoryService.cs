using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Category;
using PlantandBiologyRecognition.DAL.Payload.Respond.Category;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class CategoryService : BaseService<CategoryService>, ICategoryService
    {
        public CategoryService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<CategoryService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateCategoryRespond> CreateCategory(CreateCategoryRequest createCategoryRequest)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var newCategory = _mapper.Map<Category>(createCategoryRequest);
                    await _unitOfWork.GetRepository<Category>().InsertAsync(newCategory);
                    return _mapper.Map<CreateCategoryRespond>(newCategory);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<DeleteCategoryRespond> DeleteCategory(DeleteCategoryRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Category>();
                var category = await repo.GetByIdAsync(request.CategoryId);
                if (category == null)
                    throw new NotFoundException("Category not found");

                repo.DeleteAsync(category);

                return new DeleteCategoryRespond
                {
                    Success = true,
                    Message = $"Category deleted successfully. Reason: {request.Reason}"
                };
            });
        }

        public async Task<List<GetCategoryRespond>> GetAllCategories()
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetListAsync();
            return categories.Select(c => _mapper.Map<GetCategoryRespond>(c)).ToList();
        }

        public async Task<GetCategoryRespond> GetCategoryById(Guid categoryId)
        {
            var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(categoryId);
            if (category == null)
                throw new NotFoundException("Category not found");

            return _mapper.Map<GetCategoryRespond>(category);
        }

        public async Task<UpdateCategoryRespond> UpdateCategory(UpdateCategoryRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Category>();
                var category = await repo.GetByIdAsync(request.CategoryId);
                if (category == null)
                    throw new NotFoundException("Category not found");

                category.Name = request.Name;
                repo.UpdateAsync(category);
                return _mapper.Map<UpdateCategoryRespond>(category);
            });
        }
    }
} 