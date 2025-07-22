using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Google.Apis.Gmail.v1;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Auth;
using PlantandBiologyRecognition.DAL.Payload.Request.Email;
using PlantandBiologyRecognition.DAL.Payload.Respond.Auth;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using static PlantandBiologyRecognition.API.Constants.ApiEndPointConstant;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;
        private readonly IRefreshTokensService _refreshTokensService;
        private readonly IEmailService _emailService;
        private readonly IOtpService _otpService;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public AuthController(
            IAuthService authService,
            IRefreshTokensService refreshTokensService,
            IEmailService emailService,
            IOtpService otpService,
            IUnitOfWork<AppDbContext> unitOfWork,
            ILogger<AuthController> logger)
            : base(logger)
        {
            _authService = authService;
            _refreshTokensService = refreshTokensService;
            _emailService = emailService;
            _otpService = otpService;
            _unitOfWork = unitOfWork;
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

        [HttpPost(ApiEndPointConstant.Auth.ResetPasswordWithOtp)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPasswordWithOtp([FromBody] ResetPasswordWithOtpRequest request)
        {
            try
            {
                var otpValidation = await _otpService.ValidateOtp(request.Email, request.OtpCode);
                if (!otpValidation.Success)
                {
                    return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "Invalid or expired OTP",
                        $"Attempts left: {otpValidation.AttemptsLeft}"
                    ));
                }

                var userRepo = _unitOfWork.GetRepository<User>();
                var user = await userRepo.SingleOrDefaultAsync(
                    predicate: u => u.Email == request.Email
                );
                if (user == null)
                {
                    return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "User not found",
                        "No user exists with the provided email"
                    ));
                }

                // Hashing logic — replace this with your actual hashing function
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

                 userRepo.UpdateAsync(user);
                await _unitOfWork.CommitAsync();

                return Ok(ApiResponseBuilder.BuildResponse<object>(
                    StatusCodes.Status200OK,
                    "Password reset successful",
                    null
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "Password reset failed",
                    ex.Message
                ));
            }
        }

        [HttpPost(ApiEndPointConstant.Auth.ForgotPassword)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                // Check if user exists
                var userRepo = _unitOfWork.GetRepository<User>();
                var user = await userRepo.SingleOrDefaultAsync(predicate: u => u.Email == request.Email);

                if (user == null)
                {
                    return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "Email not registered",
                        "No user found with this email."
                    ));
                }

                // Send OTP email
                await _emailService.SendOtpEmailAsync(new SendOtpEmailRequest
                {
                    Email = request.Email
                });

                return Ok(ApiResponseBuilder.BuildResponse<object>(
                    StatusCodes.Status200OK,
                    "OTP sent to your email. Use it to reset your password.",
                    null
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status500InternalServerError,
                    "Failed to send OTP for password reset.",
                    ex.Message
                ));
            }
        }



        [HttpGet("oauth2/google-login")]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {
            if (!string.IsNullOrEmpty(returnUrl) && !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = "/";
            }
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse", new { returnUrl }) };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("oauth2/google-response")]
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
                if (!authenticateResult.Succeeded)
                {
                    return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "External authentication failed",
                        authenticateResult.Failure?.Message ?? "Unknown authentication error"
                    ));
                }
                var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
                var name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "Email claim missing",
                        "Email claim is required for OAuth2 login"
                    ));
                }
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "Name claim missing",
                        "Name claim is required for OAuth2 login"
                    ));
                }
                var loginResponse = await _authService.LoginWithOAuth2Async(email, name);
                return Ok(ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status200OK,
                    "OAuth2 login successful",
                    loginResponse
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status400BadRequest,
                    "OAuth2 login failed",
                    ex.Message
                ));
            }
        }
    }
}