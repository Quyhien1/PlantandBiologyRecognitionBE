using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using PlantandBiologyRecognition.DAL.Models;
using System.Security.Claims;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class CloudinaryService : BaseService<CloudinaryService>, ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        const long MaxImageSize = 5 * 1024 * 1024;
        const long MaxFileSize = 100 * 1024 * 1024;

        public CloudinaryService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<CloudinaryService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _configuration = configuration;
            var cloudName = _configuration["Cloudinary:CloudName"];
            var apiKey = _configuration["Cloudinary:ApiKey"];
            var apiSecret = _configuration["Cloudinary:ApiSecret"];

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
                throw new ArgumentException("Cloudinary configuration is missing");

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file, ClaimsPrincipal user)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file format. Allowed formats: .jpg, .jpeg, .png");

            if (file.Length > MaxImageSize)
                throw new ArgumentException("File size exceeds the 5MB limit.");

            // Comment out authorization code
            /*
            var userIdClaim = user.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Invalid token or missing user ID.");

            var userEntity = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId);
            if (userEntity == null)
                throw new KeyNotFoundException("User not found.");
            */

            try
            {
                using var stream = file.OpenReadStream();

                // Replace user-based naming with generic naming
                //var sanitizedUserName = RemoveVietnameseAccent(userEntity.Name).ToLower().Replace(" ", "_");
                var sanitizedUserName = "user";

                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                var timestamp = vietnamTime.ToString("yyyyMMddHHmmss");

                var fileName = $"{sanitizedUserName}_{timestamp}";

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = fileName,
                    Overwrite = false,
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Cloudinary upload error: {ex.Message}");
                throw new Exception("Failed to upload image.");
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, ClaimsPrincipal user)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (file.Length > MaxFileSize)
                throw new ArgumentException("File size exceeds the 100MB limit.");

            // Comment out authorization code
            /*
            var userIdClaim = user.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("Invalid token or missing user ID.");

            var userEntity = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId);
            if (userEntity == null)
                throw new KeyNotFoundException("User not found.");
            */

            try
            {
                using var stream = file.OpenReadStream();

                // Replace user-based naming with generic naming
                //var sanitizedUserName = RemoveVietnameseAccent(userEntity.Name).ToLower().Replace(" ", "_");
                var sanitizedUserName = "user";

                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                var timestamp = vietnamTime.ToString("yyyyMMddHHmmss");

                var fileName = $"{sanitizedUserName}_{timestamp}";

                var uploadParams = new AutoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = fileName,
                    Overwrite = false
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cloudinary upload error: {ex.Message}");
                throw new Exception("Failed to upload file.");
            }
        }

        private string RemoveVietnameseAccent(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            text = text.Normalize(NormalizationForm.FormD);
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            return regex.Replace(text, "").Replace("đ", "d").Replace("Đ", "D");
        }
    }
}
