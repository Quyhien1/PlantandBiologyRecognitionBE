using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.API.Validators;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleDetail;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleDetail;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class SampleDetailController : BaseController<SampleDetailController>
    {
        private readonly ISampleDetailService _sampleDetailService;

        public SampleDetailController(ISampleDetailService sampleDetailService, ILogger<SampleDetailController> logger)
            : base(logger)
        {
            _sampleDetailService = sampleDetailService;
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpPost(ApiEndPointConstant.SampleDetails.CreateSampleDetail)]
        [ProducesResponseType(typeof(ApiResponse<CreateSampleDetailRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSampleDetail([FromForm] CreateSampleDetailRequest createSampleDetailRequest)
        {
            var response = await _sampleDetailService.CreateSampleDetail(createSampleDetailRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create sample detail",
                    "The sample detail creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateSampleDetail), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Sample detail created successfully",
                response
            ));
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.SampleDetails.GetSampleDetailById)]
        [ProducesResponseType(typeof(ApiResponse<GetSampleDetailRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSampleDetailById(Guid id)
        {
            try
            {
                var response = await _sampleDetailService.GetSampleDetailById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample detail retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample detail not found", "NotFound"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.SampleDetails.GetAllSampleDetails)]
        [ProducesResponseType(typeof(ApiResponse<List<GetSampleDetailRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSampleDetail([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var response = await _sampleDetailService.GetAllSampleDetails(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(200, "All sample details retrieved", response));
        }

        [HttpPut(ApiEndPointConstant.SampleDetails.UpdateSampleDetail)]
        [ProducesResponseType(typeof(ApiResponse<UpdateSampleDetailRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSampleDetail([FromForm] UpdateSampleDetailRequest request)
        {
            try
            {
                var response = await _sampleDetailService.UpdateSampleDetail(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample detail updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample detail not found", "NotFound"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpDelete(ApiEndPointConstant.SampleDetails.DeleteSampleDetail)]
        [ProducesResponseType(typeof(ApiResponse<DeleteSampleDetailRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSampleDetail([FromForm] DeleteSampleDetailRequest request)
        {
            try
            {
                var response = await _sampleDetailService.DeleteSampleDetail(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Sample detail deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Sample detail not found", "NotFound"
                ));
            }
        }
    }
} 