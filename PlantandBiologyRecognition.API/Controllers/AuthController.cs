using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Payload.Request.Auth;
using PlantandBiologyRecognition.DAL.Payload.Respond.Auth;
using static PlantandBiologyRecognition.API.Constants.ApiEndPointConstant;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;
        private readonly IRefreshTokensService _refreshTokensService;

        public AuthController(
            IAuthService authService,
            IRefreshTokensService refreshTokensService,
            ILogger<AuthController> logger)
            : base(logger)
        {
            _authService = authService;
            _refreshTokensService = refreshTokensService;
        }

        [HttpPost(ApiEndPointConstant.Auth.Login)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var response = await _authService.Login(loginRequest);
                return Ok(ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status200OK,
                    "Login successful",
                    response
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Login failed",
                    ex.Message
                ));
            }
        }

        [HttpPost(ApiEndPointConstant.Auth.RefreshToken)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var newAccessToken = await _refreshTokensService.RefreshAccessToken(request.RefreshToken);
                return Ok(ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status200OK,
                    "Token refreshed successfully",
                    new { AccessToken = newAccessToken }
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Refresh token failed",
                    ex.Message
                ));
            }
        }
        
        [HttpPost(ApiEndPointConstant.Auth.LogOut)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var result = await _refreshTokensService.DeleteRefreshToken(request.RefreshToken);
                return Ok(ApiResponseBuilder.BuildResponse<object>(
                    StatusCodes.Status200OK,
                    result ? "Logout successful" : "No active session found",
                    null
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Logout failed",
                    ex.Message
                ));
            }
        }
    }
}