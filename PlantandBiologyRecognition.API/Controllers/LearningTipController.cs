using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.API.Validators;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.LearningTip;
using PlantandBiologyRecognition.DAL.Payload.Respond.LearningTip;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class LearningTipController : BaseController<LearningTipController>
    {
        private readonly ILearningTipService _learningTipService;

        public LearningTipController(ILearningTipService learningTipService, ILogger<LearningTipController> logger)
            : base(logger)
        {
            _learningTipService = learningTipService;
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Teacher)]
        [HttpPost(ApiEndPointConstant.LearningTips.CreateLearningTip)]
        [ProducesResponseType(typeof(ApiResponse<CreateLearningTipRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateLearningTip([FromForm] CreateLearningTipRequest createLearningTipRequest)
        {
            var response = await _learningTipService.CreateLearningTip(createLearningTipRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create learning tip",
                    "The learning tip creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateLearningTip), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Learning tip created successfully",
                response
            ));
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.LearningTips.GetLearningTipById)]
        [ProducesResponseType(typeof(ApiResponse<GetLearningTipRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLearningTipById(Guid id)
        {
            try
            {
                var response = await _learningTipService.GetLearningTipById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Learning tip retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Learning tip not found", "NotFound"
                ));
            }
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpGet(ApiEndPointConstant.LearningTips.GetAllLearningTips)]
        [ProducesResponseType(typeof(ApiResponse<List<GetLearningTipRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLearningTips([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var response = await _learningTipService.GetAllLearningTips(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(200, "All learning tips retrieved", response));
        }

        [HttpPut(ApiEndPointConstant.LearningTips.UpdateLearningTip)]
        [ProducesResponseType(typeof(ApiResponse<UpdateLearningTipRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLearningTip([FromForm] UpdateLearningTipRequest request)
        {
            try
            {
                var response = await _learningTipService.UpdateLearningTip(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Learning tip updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Learning tip not found", "NotFound"
                ));
            }
        }

        [HttpDelete(ApiEndPointConstant.LearningTips.DeleteLearningTip)]
        [ProducesResponseType(typeof(ApiResponse<DeleteLearningTipRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLearningTip([FromForm] DeleteLearningTipRequest request)
        {
            try
            {
                var response = await _learningTipService.DeleteLearningTip(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Learning tip deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Learning tip not found", "NotFound"
                ));
            }
        }
    }
} 