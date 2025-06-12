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
using PlantandBiologyRecognition.DAL.Payload.Request;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond;
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
    }
}
