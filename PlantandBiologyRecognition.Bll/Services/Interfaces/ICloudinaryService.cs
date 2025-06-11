using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file, ClaimsPrincipal user);
        Task<string> UploadFileAsync(IFormFile file, ClaimsPrincipal user);
    }
}
