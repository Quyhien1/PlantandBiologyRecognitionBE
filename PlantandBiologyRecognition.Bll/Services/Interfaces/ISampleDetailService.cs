using PlantandBiologyRecognition.DAL.Payload.Request.SampleDetail;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleDetail;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ISampleDetailService
    {
        Task<CreateSampleDetailRespond> CreateSampleDetail(CreateSampleDetailRequest request);
        Task<GetSampleDetailRespond> GetSampleDetailById(Guid id);
        Task<List<GetSampleDetailRespond>> GetAllSampleDetails();
        Task<UpdateSampleDetailRespond> UpdateSampleDetail(UpdateSampleDetailRequest request);
        Task<DeleteSampleDetailRespond> DeleteSampleDetail(DeleteSampleDetailRequest request);
    }
} 