using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Feedback;
using PlantandBiologyRecognition.DAL.Payload.Respond.Feedback;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<CreateFeedbackRespond> CreateFeedback(CreateFeedbackRequest createFeedbackRequest);
        Task<GetFeedbackRespond> GetFeedbackById(Guid feedbackId);
        Task<IPaginate<GetFeedbackRespond>> GetAllFeedbacks(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateFeedbackRespond> UpdateFeedback(UpdateFeedbackRequest updateFeedbackRequest);
        Task<DeleteFeedbackRespond> DeleteFeedback(DeleteFeedbackRequest deleteFeedbackRequest);
    }
}
