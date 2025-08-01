using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.API.Validators;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Sample;
using PlantandBiologyRecognition.DAL.Payload.Respond.Sample;

namespace PlantandBiologyRecognition.API.Controllers
{
    [ApiController]
    public class SampleController : BaseController<SampleController>
    {
        private readonly ISampleService _sampleService;
        
        public SampleController(ISampleService sampleService, ILogger<SampleController> logger)
            : base(logger)
        {
            _sampleService = sampleService;
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpPost(ApiEndPointConstant.Samples.CreateSample)]
        [ProducesResponseType(typeof(ApiResponse<CreateSampleRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSample([FromForm] CreateSampleRequest createSampleRequest)
        {
            var response = await _sampleService.CreateSample(createSampleRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create sample",
                    "The sample creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateSample), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Sample created successfully",
                response
            ));
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.Samples.GetSampleById)]
        [ProducesResponseType(typeof(ApiResponse<GetSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSampleById(Guid id)
        {
            try
            {
                var response = await _sampleService.GetSampleById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample not found", "NotFound"
                ));
            }
        }

        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.Samples.GetAllSamples)]
        [ProducesResponseType(typeof(ApiResponse<List<GetSampleRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSample([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var response = await _sampleService.GetAllSamples(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(200, "All samples retrieved", response));
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.Samples.GetSampleByName)]
        [ProducesResponseType(typeof(ApiResponse<GetSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSampleByName([FromQuery] string name)
        {
            try
            {
                var response = await _sampleService.GetSampleByName(name);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample retrieved", response));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 400, ex.Message, "BadRequest"
                ));
            }
            catch (NotFoundException)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, $"Sample with name '{name}' not found", "NotFound"
                ));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 500, "An error occurred while retrieving the sample", "InternalServerError"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpPut(ApiEndPointConstant.Samples.UpdateSample)]
        [ProducesResponseType(typeof(ApiResponse<UpdateSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSample([FromForm] UpdateSampleRequest request)
        {
            try
            {
                var response = await _sampleService.UpdateSample(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample not found", "NotFound"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpDelete(ApiEndPointConstant.Samples.DeleteSample)]
        [ProducesResponseType(typeof(ApiResponse<DeleteSampleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSample([FromForm] DeleteSampleRequest request)
        {
            try
            {
                var response = await _sampleService.DeleteSample(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample not found", "NotFound"
                ));
            }
        }
    }
} 