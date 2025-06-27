using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Payload.Request.SavedSample;
using PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class SavedSampleController : BaseController<SavedSampleController>
    {
        private readonly ISavedSampleService _savedSampleService;

        public SavedSampleController(ISavedSampleService savedSampleService, ILogger<SavedSampleController> logger)
            : base(logger)
        {
            _savedSampleService = savedSampleService;
        }

        [HttpPost(ApiEndPointConstant.SavedSamples.CreateSavedSample)]
        [ProducesResponseType(typeof(ApiResponse<CreateSavedSampleRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSavedSample([FromForm] CreateSavedSampleRequest createSavedSampleRequest)
        {
            var response = await _savedSampleService.CreateSavedSample(createSavedSampleRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create saved sample",
                    "The saved sample creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateSavedSample), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Saved sample created successfully",
                response
            ));
        }

        [HttpGet(ApiEndPointConstant.SavedSamples.GetSavedSampleById)]
        [ProducesResponseType(typeof(ApiResponse<GetSavedSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSavedSampleById(Guid id)
        {
            try
            {
                var response = await _savedSampleService.GetSavedSampleById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Saved sample retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Saved sample not found", "NotFound"
                ));
            }
        }

        [HttpGet(ApiEndPointConstant.SavedSamples.GetAllSavedSamples)]
        [ProducesResponseType(typeof(ApiResponse<List<GetSavedSampleRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSavedSamples()
        {
            var response = await _savedSampleService.GetAllSavedSamples();
            return Ok(ApiResponseBuilder.BuildResponse(200, "All saved samples retrieved", response));
        }

        [HttpPut(ApiEndPointConstant.SavedSamples.UpdateSavedSample)]
        [ProducesResponseType(typeof(ApiResponse<UpdateSavedSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSavedSample([FromForm] UpdateSavedSampleRequest request)
        {
            try
            {
                var response = await _savedSampleService.UpdateSavedSample(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Saved sample updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Saved sample not found", "NotFound"
                ));
            }
        }

        [HttpDelete(ApiEndPointConstant.SavedSamples.DeleteSavedSample)]
        [ProducesResponseType(typeof(ApiResponse<DeleteSavedSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSavedSample([FromForm] DeleteSavedSampleRequest request)
        {
            try
            {
                var response = await _savedSampleService.DeleteSavedSample(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Saved sample deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Saved sample not found", "NotFound"
                ));
            }
        }
    }
} 