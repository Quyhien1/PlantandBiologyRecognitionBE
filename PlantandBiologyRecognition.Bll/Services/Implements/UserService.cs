using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class UserService : BaseService<UserService>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ICloudinaryService _cloudinaryService;
        private const string DefaultProfilePicture = "https://res.cloudinary.com/dowx7evfg/image/upload/v1749615087/plant-logo-icon-design-free-vector_jctwfu.jpg";
        public UserService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICloudinaryService cloudinaryService)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<CreateUserRespond> CreateUser(CreateUserRequest createUserRequest)
        {
            try
            {
                var existingUser = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: s => s.Email == createUserRequest.Email);
                if (existingUser != null)
                {
                    throw new BusinessException("Email is already in use. Please use a different email.");
                }
                if (createUserRequest == null)
                {
                    throw new ArgumentNullException(nameof(createUserRequest), "Create user request cannot be null.");
                }
                var newUser = _mapper.Map<User>(createUserRequest);
                newUser.UserId = Guid.NewGuid();
                newUser.CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
                newUser.PasswordHash = PasswordUtil.HashPassword(createUserRequest.Password);
                newUser.IsActive = true;
                var user = _httpContextAccessor.HttpContext?.User;
                if (createUserRequest.Avatar != null && user != null)
                {
                    newUser.Avatar = await _cloudinaryService.UploadImageAsync(createUserRequest.Avatar, user);
                }
                else
                {
                    newUser.Avatar = DefaultProfilePicture;
                }
                await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CreateUserRespond>(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user: {Message}", ex.Message);
                throw;
            }
        }
    }
}
