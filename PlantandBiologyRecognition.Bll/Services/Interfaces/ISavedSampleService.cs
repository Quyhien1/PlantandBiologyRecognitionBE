using PlantandBiologyRecognition.DAL.Payload.Request.SavedSample;
using PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISavedSampleService
    {
        Task<CreateSavedSampleRespond> CreateSavedSample(CreateSavedSampleRequest request);
        Task<GetSavedSampleRespond> GetSavedSampleById(Guid id);
        Task<List<GetSavedSampleRespond>> GetAllSavedSamples();
        Task<UpdateSavedSampleRespond> UpdateSavedSample(UpdateSavedSampleRequest request);
        Task<DeleteSavedSampleRespond> DeleteSavedSample(DeleteSavedSampleRequest request);
    }
} 