using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IRefreshTokensService
    {
        Task<string> GenerateAndStoreRefreshToken(Guid userId);
        Task<string> RefreshAccessToken(string refreshToken);
        Task<bool> DeleteRefreshToken(string refreshToken);
    }
}
