using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Feedback;
using PlantandBiologyRecognition.DAL.Payload.Respond.Feedback;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class FeedbackController : BaseController<FeedbackController>
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService, ILogger<FeedbackController> logger)
            : base(logger)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost(ApiEndPointConstant.Feedbacks.CreateFeedback)]
        [ProducesResponseType(typeof(ApiResponse<CreateFeedbackRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFeedback([FromForm] CreateFeedbackRequest createFeedbackRequest)
        {
            var response = await _feedbackService.CreateFeedback(createFeedbackRequest);
            if (response == null)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Failed to create feedback",
                    "The feedback creation process failed"
                ));
            }

            return CreatedAtAction(nameof(CreateFeedback), ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                "Feedback created successfully",
                response
            ));
        }

        [HttpGet(ApiEndPointConstant.Feedbacks.GetFeedbackById)]
        [ProducesResponseType(typeof(ApiResponse<GetFeedbackRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFeedbackById(Guid id)
        {
            try
            {
                var response = await _feedbackService.GetFeedbackById(id);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Feedback retrieved", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Feedback not found", "NotFound"
                ));
            }
        }

        [HttpGet(ApiEndPointConstant.Feedbacks.GetAllFeedbacks)]
        [ProducesResponseType(typeof(ApiResponse<IPaginate<GetFeedbackRespond>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFeedbacks([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var response = await _feedbackService.GetAllFeedbacks(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(200, "All feedbacks retrieved", response));
        }

        [HttpPut(ApiEndPointConstant.Feedbacks.UpdateFeedback)]
        [ProducesResponseType(typeof(ApiResponse<UpdateFeedbackRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateFeedback([FromForm] UpdateFeedbackRequest request)
        {
            try
            {
                var response = await _feedbackService.UpdateFeedback(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Feedback updated successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Feedback not found", "NotFound"
                ));
            }
        }

        [HttpDelete(ApiEndPointConstant.Feedbacks.DeleteFeedback)]
        [ProducesResponseType(typeof(ApiResponse<DeleteFeedbackRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFeedback([FromForm] DeleteFeedbackRequest request)
        {
            try
            {
                var response = await _feedbackService.DeleteFeedback(request);
                return Ok(ApiResponseBuilder.BuildResponse(200, "Feedback deleted successfully", response));
            }
            catch (Exception)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null, 404, "Feedback not found", "NotFound"
                ));
            }
        }
    }
}
