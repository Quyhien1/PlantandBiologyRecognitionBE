using PlantandBiologyRecognition.DAL.Payload.Request.SampleImage;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISampleImageService
    {
        Task<CreateSampleImageRespond> CreateSampleImage(CreateSampleImageRequest request);
        Task<GetSampleImageRespond> GetSampleImageById(Guid id);
        Task<List<GetSampleImageRespond>> GetAllSampleImages();
        Task<UpdateSampleImageRespond> UpdateSampleImage(UpdateSampleImageRequest request);
        Task<DeleteSampleImageRespond> DeleteSampleImage(DeleteSampleImageRequest request);
    }
} 