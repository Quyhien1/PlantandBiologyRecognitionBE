using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.LearningTip;
using PlantandBiologyRecognition.DAL.Payload.Respond.LearningTip;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ILearningTipService
    {
        Task<CreateLearningTipRespond> CreateLearningTip(CreateLearningTipRequest createLearningTipRequest);
        Task<GetLearningTipRespond> GetLearningTipById(Guid tipId);
        Task<IPaginate<GetLearningTipRespond>> GetAllLearningTips(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateLearningTipRespond> UpdateLearningTip(UpdateLearningTipRequest updateLearningTipRequest);
        Task<DeleteLearningTipRespond> DeleteLearningTip(DeleteLearningTipRequest deleteLearningTipRequest);
    }
} 