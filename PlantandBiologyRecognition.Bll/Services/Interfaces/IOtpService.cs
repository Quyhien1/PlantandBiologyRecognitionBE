using PlantandBiologyRecognition.DAL.Payload.Respond.Otp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IOtpService
    {
        Task CreateOtpEntity(string email, string otp);
        Task<ValidateOtpRespond> ValidateOtp(string email, string otp);
    }
}
