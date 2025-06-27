using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.SavedSample;
using PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISavedSampleService
    {
        Task<CreateSavedSampleRespond> CreateSavedSample(CreateSavedSampleRequest request);
        Task<GetSavedSampleRespond> GetSavedSampleById(Guid id);
        Task<IPaginate<GetSavedSampleRespond>> GetAllSavedSamples(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateSavedSampleRespond> UpdateSavedSample(UpdateSavedSampleRequest request);
        Task<DeleteSavedSampleRespond> DeleteSavedSample(DeleteSavedSampleRequest request);
    }
} 