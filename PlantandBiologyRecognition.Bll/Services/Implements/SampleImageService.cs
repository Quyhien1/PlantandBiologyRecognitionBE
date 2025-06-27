using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleImage;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleImage;
using PlantandBiologyRecognition.DAL.Repositories;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class SampleImageService : BaseService<SampleImageService>, ISampleImageService
    {
        public SampleImageService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<SampleImageService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateSampleImageRespond> CreateSampleImage(CreateSampleImageRequest request)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var sampleImage = _mapper.Map<Sampleimage>(request);
                    await _unitOfWork.GetRepository<Sampleimage>().InsertAsync(sampleImage);
                    return _mapper.Map<CreateSampleImageRespond>(sampleImage);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating sample image", ex);
            }
        }

        public async Task<GetSampleImageRespond> GetSampleImageById(Guid id)
        {
            var sampleImage = await _unitOfWork.GetRepository<Sampleimage>().GetByIdAsync(id);
            if (sampleImage == null)
                throw new NotFoundException("Sample image not found");
            return _mapper.Map<GetSampleImageRespond>(sampleImage);
        }

        public async Task<List<GetSampleImageRespond>> GetAllSampleImages()
        {
            var sampleImages = await _unitOfWork.GetRepository<Sampleimage>().GetListAsync();
            return sampleImages.Select(si => _mapper.Map<GetSampleImageRespond>(si)).ToList();
        }

        public async Task<UpdateSampleImageRespond> UpdateSampleImage(UpdateSampleImageRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Sampleimage>();
                var sampleImage = await repo.GetByIdAsync(request.Id);
                if (sampleImage == null)
                    throw new NotFoundException("Sample image not found");
                if (!string.IsNullOrEmpty(request.ImageUrl))
                    sampleImage.ImageUrl = request.ImageUrl;
                if (!string.IsNullOrEmpty(request.Description))
                    sampleImage.Description = request.Description;

                repo.UpdateAsync(sampleImage);
                return _mapper.Map<UpdateSampleImageRespond>(sampleImage);
            });
        }

        public async Task<DeleteSampleImageRespond> DeleteSampleImage(DeleteSampleImageRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Sampleimage>();
                var sampleImage = await repo.GetByIdAsync(request.Id);
                if (sampleImage == null)
                    throw new NotFoundException("Sample image not found");

                repo.DeleteAsync(sampleImage);
                return _mapper.Map<DeleteSampleImageRespond>(sampleImage);
            });
        }
    }
}