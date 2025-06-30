using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.API.Validators;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleImage;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class SampleImageController : BaseController<SampleImageController>
    {
        private readonly ISampleImageService _sampleImageService;

        public SampleImageController(ISampleImageService sampleImageService, ILogger<SampleImageController> logger)
            : base(logger)
        {
            _sampleImageService = sampleImageService;
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpPost(ApiEndPointConstant.SampleImages.CreateSampleImage)]
        [ProducesResponseType(typeof(ApiResponse<CreateSampleImageRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSampleImage([FromForm] CreateSampleImageRequest createSampleImageRequest)
        {
            var response = await _sampleImageService.CreateSampleImage(createSampleImageRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create sample image",
                    "The sample image creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateSampleImage), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Sample image created successfully",
                response
            ));
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.SampleImages.GetSampleImageById)]
        [ProducesResponseType(typeof(ApiResponse<GetSampleImageRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSampleImageById(Guid id)
        {
            try
            {
                var response = await _sampleImageService.GetSampleImageById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample image retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample image not found", "NotFound"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.SampleImages.GetAllSampleImages)]
        [ProducesResponseType(typeof(ApiResponse<List<GetSampleImageRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSampleImage([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var response = await _sampleImageService.GetAllSampleImages(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(200, "All sample images retrieved", response));
        }

        [HttpPut(ApiEndPointConstant.SampleImages.UpdateSampleImage)]
        [ProducesResponseType(typeof(ApiResponse<UpdateSampleImageRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSampleImage([FromForm] UpdateSampleImageRequest request)
        {
            try
            {
                var response = await _sampleImageService.UpdateSampleImage(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample image updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample image not found", "NotFound"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpDelete(ApiEndPointConstant.SampleImages.DeleteSampleImage)]
        [ProducesResponseType(typeof(ApiResponse<DeleteSampleImageRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSampleImage([FromForm] DeleteSampleImageRequest request)
        {
            try
            {
                var response = await _sampleImageService.DeleteSampleImage(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample image deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample image not found", "NotFound"
                ));
            }
        }
    }
} 