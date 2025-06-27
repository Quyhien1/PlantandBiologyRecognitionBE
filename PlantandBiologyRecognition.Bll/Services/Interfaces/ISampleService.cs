using PlantandBiologyRecognition.DAL.Payload.Request.Sample;
using PlantandBiologyRecognition.DAL.Payload.Respond.Sample;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISampleService
    {
        Task<CreateSampleRespond> CreateSample(CreateSampleRequest request);
        Task<GetSampleRespond> GetSampleById(Guid id);
        Task<List<GetSampleRespond>> GetAllSamples();
        Task<UpdateSampleRespond> UpdateSample(UpdateSampleRequest request);
        Task<DeleteSampleRespond> DeleteSample(DeleteSampleRequest request);
    }
} 