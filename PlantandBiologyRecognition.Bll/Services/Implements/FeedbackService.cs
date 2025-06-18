using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.Feedback;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.Feedback;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class FeedbackService : BaseService<FeedbackService>, IFeedbackService
    {
        public FeedbackService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<FeedbackService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
        public async Task<CreateFeedbackRespond> CreateFeedback(CreateFeedbackRequest createFeedbackRequest)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var newFeedback = _mapper.Map<Feedback>(createFeedbackRequest);
                    await _unitOfWork.GetRepository<Feedback>().InsertAsync(newFeedback);
                    return _mapper.Map<CreateFeedbackRespond>(newFeedback);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating feedback: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<DeleteFeedbackRespond> DeleteFeedback(DeleteFeedbackRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Feedback>();
                var feedback = await repo.GetByIdAsync(request.FeedbackId);
                if (feedback == null)
                    throw new NotFoundException("Feedback not found");

                repo.DeleteAsync(feedback);

                return new DeleteFeedbackRespond
                {
                    Success = true,
                    Message = $"Feedback deleted successfully. Reason: {request.Reason}"
                };
            });
        }


        public async Task<List<GetFeedbackRespond>> GetAllFeedbacks()
        {
            var feedbacks = await _unitOfWork.GetRepository<Feedback>().GetListAsync();
            return feedbacks.Select(fb => _mapper.Map<GetFeedbackRespond>(fb)).ToList();
        }

        public async Task<GetFeedbackRespond> GetFeedbackById(Guid feedbackId)
        {
            var feedback = await _unitOfWork.GetRepository<Feedback>().GetByIdAsync(feedbackId);
            if (feedback == null)
                throw new NotFoundException("Feedback not found");

            return _mapper.Map<GetFeedbackRespond>(feedback);
        }

        public async Task<UpdateFeedbackRespond> UpdateFeedback(UpdateFeedbackRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Feedback>();
                var feedback = await repo.GetByIdAsync(request.FeedbackId);
                if (feedback == null)
                    throw new NotFoundException("Feedback not found");

                feedback.Message = request.Message;
                feedback.SubmittedAt = DateTime.Now;

                repo.UpdateAsync(feedback);
                return _mapper.Map<UpdateFeedbackRespond>(feedback);
            });
        }

    }
}
