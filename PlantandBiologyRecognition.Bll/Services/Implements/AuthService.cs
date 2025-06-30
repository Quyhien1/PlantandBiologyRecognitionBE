using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Auth;
using PlantandBiologyRecognition.DAL.Payload.Respond.Auth;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class AuthService : BaseService<AuthService>, IAuthService
    {
        private readonly JwtUtil _jwtUtil;
        private readonly IRefreshTokensService _refreshTokensService;
    
        public AuthService(
            IUnitOfWork<AppDbContext> unitOfWork,
            ILogger<CategoryService> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            JwtUtil jwtUtil,
            IRefreshTokensService refreshTokensService)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _jwtUtil = jwtUtil;
            _refreshTokensService = refreshTokensService;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Email == loginRequest.Email && u.IsActive);

            if (user == null)
                throw new NotFoundException($"User with email {loginRequest.Email} not found.");

            if (!PasswordUtil.VerifyPassword(loginRequest.Password, user.PasswordHash))
                throw new WrongPasswordException("Invalid password");

            var loginResponse = new LoginResponse();

            var token = _jwtUtil.GenerateJwtToken(user);
            var refreshToken = await _refreshTokensService.GenerateAndStoreRefreshToken(user.UserId);

            loginResponse.AccessToken = token;
            loginResponse.RefreshToken = refreshToken;
            return loginResponse;
        }
    }
}