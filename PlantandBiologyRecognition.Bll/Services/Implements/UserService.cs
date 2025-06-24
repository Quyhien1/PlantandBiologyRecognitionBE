using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
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
        /// <summary>
        /// Retrieves a user's details by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>The user information mapped to a <see cref="CreateUserRespond"/> object.</returns>
        public async Task<CreateUserRespond> GetUserById(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentException("User ID cannot be empty.", nameof(userId));
                }
                var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: s => s.UserId == userId);
                if (user == null)
                {
                    throw new NotFoundException($"User with ID {userId} not found.");
                }
                return _mapper.Map<CreateUserRespond>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by ID: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a paginated list of active users, optionally filtered by a search term matching name or email.
        /// </summary>
        /// <param name="page">The page number to retrieve (default is 1).</param>
        /// <param name="size">The number of users per page (default is 10).</param>
        /// <param name="searchTerm">An optional term to filter users by name or email (case-insensitive).</param>
        /// <returns>A paginated result containing user response objects matching the criteria.</returns>
        public async Task<IPaginate<CreateUserRespond>> GetAllUsers(int page = 1, int size = 10, string searchTerm = null)
        {
            try
            {
                string searchTermLower = searchTerm?.ToLower();
                
                return await _unitOfWork.GetRepository<User>().GetPagingListAsync(
                    selector: x => _mapper.Map<CreateUserRespond>(x),
                    predicate: x => x.IsActive && 
                                   (string.IsNullOrWhiteSpace(searchTerm) || 
                                    x.Name.ToLower().Contains(searchTermLower) || 
                                    x.Email.ToLower().Contains(searchTermLower)),
                    orderBy: q => q.OrderByDescending(x => x.CreatedAt),
                    page: page,
                    size: size
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users: {Message}", ex.Message);
                throw;
            }
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentException("User ID cannot be empty.", nameof(userId));
                }
                var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: s => s.UserId == userId);
                if (user == null)
                {
                    throw new NotFoundException($"User with ID {userId} not found.");
                }
                user.IsActive = false;
                _unitOfWork.GetRepository<User>().UpdateAsync(user);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user: {Message}", ex.Message);
                throw;
            }
        }
        public async Task<UpdateUserRespond> UpdateUser(Guid userId, UpdateUserRequest updateUserRequest)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentException("User ID cannot be empty.", nameof(userId));
                }
                if (updateUserRequest == null)
                {
                    throw new ArgumentNullException(nameof(updateUserRequest), "Update user request cannot be null.");
                }
                var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: s => s.UserId == userId && s.IsActive,
                    orderBy: q => q.OrderByDescending(s => s.Name)
                    );
                if (user == null)
                {
                    throw new NotFoundException($"User with ID {userId} not found.");
                }
                _mapper.Map(updateUserRequest, user);
                string currentAvatar = user.Avatar;
                if (updateUserRequest.Avatar != null)
                {
                    user.Avatar = await _cloudinaryService.UploadImageAsync(updateUserRequest.Avatar, _httpContextAccessor.HttpContext?.User);
                }
                else
                {
                    user.Avatar = currentAvatar;
                }
                _unitOfWork.GetRepository<User>().UpdateAsync(user);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<UpdateUserRespond>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {Message}", ex.Message);
                throw;
            }
        }
    }
}
