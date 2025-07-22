using PlantandBiologyRecognition.DAL.Payload.Request.Email;
using PlantandBiologyRecognition.DAL.Payload.Respond.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body);
        Task<SendOtpEmailResponse> SendOtpEmailAsync(SendOtpEmailRequest request);
    }
}
