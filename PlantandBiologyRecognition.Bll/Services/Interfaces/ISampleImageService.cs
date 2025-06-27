using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleImage;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISampleImageService
    {
        Task<CreateSampleImageRespond> CreateSampleImage(CreateSampleImageRequest request);
        Task<GetSampleImageRespond> GetSampleImageById(Guid id);
        Task<IPaginate<GetSampleImageRespond>> GetAllSampleImages(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateSampleImageRespond> UpdateSampleImage(UpdateSampleImageRequest request);
        Task<DeleteSampleImageRespond> DeleteSampleImage(DeleteSampleImageRequest request);
    }
} 