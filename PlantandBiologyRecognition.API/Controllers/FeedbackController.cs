using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Payload.Request;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;

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

        [HttpPost(ApiEndPointConstant.Feedbacks.FeedbackEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<CreateFeedbackRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFeedback([FromForm] CreateFeedbackRequest createFeedbackRequest)
        {

            var response = await _feedbackService.CreateFeedback(createFeedbackRequest);
            if (response == null)
            {
                return BadRequest(
                    ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "Failed to create feedback",
                        "The feedback creation process failed"
                    )
                );
            }
            return CreatedAtAction(
                nameof(CreateFeedback),
                ApiResponseBuilder.BuildResponse(
                StatusCodes.Status201Created,
                    "Feedback created successfully",
                    response
                )
            );
        }
    }
}
