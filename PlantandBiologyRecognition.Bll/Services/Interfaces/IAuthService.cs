using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PlantandBiologyRecognition.DAL.Payload.Request.Auth;
using PlantandBiologyRecognition.DAL.Payload.Respond.Auth;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<LoginResponse> LoginWithOAuth2Async(string email, string name);
    }
}