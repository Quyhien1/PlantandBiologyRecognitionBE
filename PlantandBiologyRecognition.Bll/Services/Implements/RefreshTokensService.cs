using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class RefreshTokensService : IRefreshTokensService
    {
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly JwtUtil _jwtUtil;

        public RefreshTokensService(IUnitOfWork<AppDbContext> unitOfWork, JwtUtil jwtUtil)
        {
            _unitOfWork = unitOfWork;
            _jwtUtil = jwtUtil;
        }

        public async Task<string> GenerateAndStoreRefreshToken(Guid userId)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                var refreshTokenRepo = _unitOfWork.GetRepository<RefreshToken>();
                var newToken = new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Token = refreshToken,
                    CreateAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                };

                await refreshTokenRepo.InsertAsync(newToken);
                return refreshToken;
            });
        }

        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var refreshTokenRepo = _unitOfWork.GetRepository<RefreshToken>();
                var storedToken = await refreshTokenRepo.SingleOrDefaultAsync(
                    predicate: t => t.Token == refreshToken);

                if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
                {
                    throw new Exception("Invalid or expired refresh token");
                }

                var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(storedToken.UserId);
                if (user == null) throw new Exception("User not found");

                return _jwtUtil.GenerateJwtToken(user);
            });
        }

        public async Task<bool> DeleteRefreshToken(string refreshToken)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var refreshTokenRepo = _unitOfWork.GetRepository<RefreshToken>();
                var tokenToDelete = await refreshTokenRepo.SingleOrDefaultAsync(
                    predicate: t => t.Token == refreshToken);

                if (tokenToDelete != null)
                {
                    refreshTokenRepo.DeleteAsync(tokenToDelete);
                    return true;
                }
                return false;
            });
        }
    }
}