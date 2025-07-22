using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Respond.Otp;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class OtpService : IOtpService
    {
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public OtpService(IUnitOfWork<AppDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOtpEntity(string email, string otp)
        {
            var otpRepository = _unitOfWork.GetRepository<Otp>();

            var existingOtp = await otpRepository.SingleOrDefaultAsync(predicate: o => o.Email == email);
            if (existingOtp != null)
            {
                otpRepository.DeleteAsync(existingOtp);
                await _unitOfWork.CommitAsync();
            }

            var otpEntity = new Otp
            {
                Id = Guid.NewGuid(),
                Email = email,
                OtpCode = otp,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                AttemptLeft = 3
            };

            await otpRepository.InsertAsync(otpEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ValidateOtpRespond> ValidateOtp(string email, string otp)
        {
            var otpRepository = _unitOfWork.GetRepository<Otp>();
            var otpEntity = await otpRepository.SingleOrDefaultAsync(predicate: o => o.Email == email);

            if (otpEntity == null)
            {
                return new ValidateOtpRespond
                {
                    Success = false,
                    AttemptsLeft = 0
                };
            }

            if (otpEntity.ExpirationTime < DateTime.UtcNow)
            {
                otpRepository.DeleteAsync(otpEntity);
                await _unitOfWork.CommitAsync();
                return new ValidateOtpRespond
                {
                    Success = false,
                    AttemptsLeft = 0
                };
            }

            if (otpEntity.OtpCode != otp)
            {
                otpEntity.AttemptLeft--;

                if (otpEntity.AttemptLeft <= 0)
                {
                    otpRepository.DeleteAsync(otpEntity);
                }
                else
                {
                    otpRepository.UpdateAsync(otpEntity);
                }

                await _unitOfWork.CommitAsync();

                return new ValidateOtpRespond
                {
                    Success = false,
                    AttemptsLeft = otpEntity.AttemptLeft
                };
            }

            otpRepository.DeleteAsync(otpEntity);
            await _unitOfWork.CommitAsync();

            return new ValidateOtpRespond
            {
                Success = true,
                AttemptsLeft = otpEntity.AttemptLeft
            };
        }
    }
}
