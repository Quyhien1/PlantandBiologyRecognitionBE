using Google.Apis.Gmail.v1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Email;
using PlantandBiologyRecognition.DAL.Payload.Respond.Email;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly OtpUtil _otpUtil;
        private readonly IOtpService _otpService;
        private readonly ILogger<EmailService> _logger;
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _senderEmail;
        private readonly string _password;

        public EmailService(
            IUnitOfWork<AppDbContext> unitOfWork,
            OtpUtil otpUtil,
            IConfiguration configuration,
            IOtpService otpService,
            ILogger<EmailService> logger)
        {
            _unitOfWork = unitOfWork;
            _otpUtil = otpUtil;
            _otpService = otpService;
            _logger = logger;

            _smtpServer = configuration["EmailSettings:Host"];
            _port = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _senderEmail = configuration["EmailSettings:SenderEmailSMTP"];
            _password = configuration["EmailSettings:SenderPassword"];
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpServer, _port))
                {
                    smtpClient.Credentials = new NetworkCredential(_senderEmail, _password);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_senderEmail, "noreply@emailservice.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(recipientEmail);
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {recipientEmail}", recipientEmail);
                throw;
            }
        }

        public async Task<SendOtpEmailResponse> SendOtpEmailAsync(SendOtpEmailRequest request)
        {
            var response = new SendOtpEmailResponse();
            try
            {
                var existingUser = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: u => u.Email == request.Email);

                if (existingUser == null)
                {
                    throw new NotFoundException($"User with email {request.Email} not found.");
                }

                var otp = _otpUtil.GenerateOtp(request.Email);
                await _otpService.CreateOtpEntity(request.Email, otp);

                string templatePath = Path.Combine(AppContext.BaseDirectory, "Services", "Templates", "OtpEmailTemplate.html");
                string body = await File.ReadAllTextAsync(templatePath);

                body = body.Replace("{OtpCode}", otp)
                           .Replace("{ExpiryTime}", "5");

                string subject = "Your OTP Code";
                await SendEmailAsync(request.Email, subject, body);

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send OTP email.");
                response.Success = false;
                throw;
            }

            return response;
        }
    }
}
