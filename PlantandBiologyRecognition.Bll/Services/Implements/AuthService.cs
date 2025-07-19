using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Auth;
using PlantandBiologyRecognition.DAL.Payload.Respond.Auth;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

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

            if (string.IsNullOrEmpty(user.PasswordHash))
                throw new WrongPasswordException("This account does not support password login. Please login via Google.");

            if (!PasswordUtil.VerifyPassword(loginRequest.Password, user.PasswordHash))
                throw new WrongPasswordException("Invalid password");

            var loginResponse = new LoginResponse();

            // Fetch user roles here
            var userRoles = await _unitOfWork.GetRepository<Userrole>()
                .GetListAsync(predicate: r => r.UserId == user.UserId);
            var roleNames = userRoles.Select(r => r.RoleName).ToList();

            // Pass user and roles to JwtUtil
            var token = _jwtUtil.GenerateJwtToken(user, roleNames);
            var refreshToken = await _refreshTokensService.GenerateAndStoreRefreshToken(user.UserId);

            loginResponse.AccessToken = token;
            loginResponse.RefreshToken = refreshToken;
            return loginResponse;
        }

        public async Task<LoginResponse> LoginWithOAuth2Async(string email, string name)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            var userRepo = _unitOfWork.GetRepository<User>();
            var userRoleRepo = _unitOfWork.GetRepository<Userrole>();
            var user = await userRepo.SingleOrDefaultAsync(
                predicate: u => u.Email == email
            );

            if (user == null)
            {
                user = new User
                {
                    UserId = Guid.NewGuid(),
                    Email = email,
                    Name = name,
                    IsActive = true,
                    PasswordHash = ""
                };
                await userRepo.InsertAsync(user);

                var userRole = new Userrole
                {
                    RoleId = Guid.NewGuid(),
                    UserId = user.UserId,
                    RoleName = "Student"
                };
                await userRoleRepo.InsertAsync(userRole);
            }

            var userRoles = await userRoleRepo.GetListAsync(
                predicate: r => r.UserId == user.UserId,
                orderBy: null,
                include: null
            );
            var roleNames = userRoles.Select(r => r.RoleName).ToList();

            var token = _jwtUtil.GenerateJwtToken(user, roleNames);
            var refreshToken = await _refreshTokensService.GenerateAndStoreRefreshToken(user.UserId);

            return new LoginResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }
        public async Task<LoginResponse> HandleGoogleLoginAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var authenticateResult = await httpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;

            return await LoginWithOAuth2Async(email, name);
        }

    }
}