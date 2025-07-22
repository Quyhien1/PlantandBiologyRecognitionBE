using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Payload.Request.Email;

namespace PlantandBiologyRecognition.API.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class EmailController : ControllerBase
        {
            public readonly IEmailService _emailService;

            public EmailController(IEmailService emailService)
            {
                _emailService = emailService;
            }

            [HttpPost(ApiEndPointConstant.Email.SendOtp)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> SendOtp([FromBody] SendOtpEmailRequest request)
            {
                try
                {
                    var response = await _emailService.SendOtpEmailAsync(request);
                    return Ok(ApiResponseBuilder.BuildResponse(
                        StatusCodes.Status200OK,
                        "OTP sent successfully.",
                        response
                    ));
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status404NotFound,
                        $"Staff with email {request.Email} not found.",
                        ex.Message
                    ));
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status500InternalServerError,
                        "Failed to send OTP email.",
                        ex.Message
                    ));
                }
            }
        }
    }

