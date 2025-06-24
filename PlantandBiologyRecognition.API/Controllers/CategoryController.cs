using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Category;
using PlantandBiologyRecognition.DAL.Payload.Respond.Category;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
            : base(logger)
        {
            _categoryService = categoryService;
        }

        [HttpPost(ApiEndPointConstant.Categories.CreateCategory)]
        [ProducesResponseType(typeof(ApiResponse<CreateCategoryRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequest createCategoryRequest)
        {
            var response = await _categoryService.CreateCategory(createCategoryRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create category",
                    "The category creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateCategory), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Category created successfully",
                response
            ));
        }

        [HttpGet(ApiEndPointConstant.Categories.GetCategoryById)]
        [ProducesResponseType(typeof(ApiResponse<GetCategoryRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var response = await _categoryService.GetCategoryById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Category retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Category not found", "NotFound"
                ));
            }
        }

        [HttpGet(ApiEndPointConstant.Categories.GetAllCategories)]
        [ProducesResponseType(typeof(ApiResponse<IPaginate<GetCategoryRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var response = await _categoryService.GetAllCategories(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(200, "All categories retrieved", response));
        }

        [HttpPut(ApiEndPointConstant.Categories.UpdateCategory)]
        [ProducesResponseType(typeof(ApiResponse<UpdateCategoryRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryRequest request)
        {
            try
            {
                var response = await _categoryService.UpdateCategory(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Category updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Category not found", "NotFound"
                ));
            }
        }

        [HttpDelete(ApiEndPointConstant.Categories.DeleteCategory)]
        [ProducesResponseType(typeof(ApiResponse<DeleteCategoryRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory([FromForm] DeleteCategoryRequest request)
        {
            try
            {
                var response = await _categoryService.DeleteCategory(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Category deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Category not found", "NotFound"
                ));
            }
        }
    }
} 