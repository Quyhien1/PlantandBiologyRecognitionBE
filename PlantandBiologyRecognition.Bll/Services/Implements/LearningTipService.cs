using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.LearningTip;
using PlantandBiologyRecognition.DAL.Payload.Respond.LearningTip;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class LearningTipService : BaseService<LearningTipService>, ILearningTipService
    {
        public LearningTipService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<LearningTipService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateLearningTipRespond> CreateLearningTip(CreateLearningTipRequest createLearningTipRequest)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var newLearningTip = _mapper.Map<Learningtip>(createLearningTipRequest);
                    await _unitOfWork.GetRepository<Learningtip>().InsertAsync(newLearningTip);
                    return _mapper.Map<CreateLearningTipRespond>(newLearningTip);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating learning tip: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<DeleteLearningTipRespond> DeleteLearningTip(DeleteLearningTipRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Learningtip>();
                var learningTip = await repo.GetByIdAsync(request.TipId);
                if (learningTip == null)
                    throw new NotFoundException("Learning tip not found");

                repo.DeleteAsync(learningTip);

                return new DeleteLearningTipRespond
                {
                    Success = true,
                    Message = $"Learning tip deleted successfully. Reason: {request.Reason}"
                };
            });
        }

        public async Task<List<GetLearningTipRespond>> GetAllLearningTips()
        {
            var learningTips = await _unitOfWork.GetRepository<Learningtip>().GetListAsync();
            return learningTips.Select(lt => _mapper.Map<GetLearningTipRespond>(lt)).ToList();
        }

        public async Task<GetLearningTipRespond> GetLearningTipById(Guid tipId)
        {
            var learningTip = await _unitOfWork.GetRepository<Learningtip>().GetByIdAsync(tipId);
            if (learningTip == null)
                throw new NotFoundException("Learning tip not found");

            return _mapper.Map<GetLearningTipRespond>(learningTip);
        }

        public async Task<UpdateLearningTipRespond> UpdateLearningTip(UpdateLearningTipRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Learningtip>();
                var learningTip = await repo.GetByIdAsync(request.TipId);
                if (learningTip == null)
                    throw new NotFoundException("Learning tip not found");

                learningTip.TipText = request.TipText;
                repo.UpdateAsync(learningTip);
                return _mapper.Map<UpdateLearningTipRespond>(learningTip);
            });
        }
    }
} 