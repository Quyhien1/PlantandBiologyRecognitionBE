using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Sample;
using PlantandBiologyRecognition.DAL.Payload.Respond.Sample;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISampleService
    {
        Task<CreateSampleRespond> CreateSample(CreateSampleRequest request);
        Task<GetSampleRespond> GetSampleById(Guid id);
        Task<GetSampleRespond> GetSampleByName(string name);
        Task<IPaginate<GetSampleRespond>> GetAllSamples(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateSampleRespond> UpdateSample(UpdateSampleRequest request);
        Task<DeleteSampleRespond> DeleteSample(DeleteSampleRequest request);
    }
} 