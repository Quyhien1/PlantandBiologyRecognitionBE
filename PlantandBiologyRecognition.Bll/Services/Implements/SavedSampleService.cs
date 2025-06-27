using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.SavedSample;
using PlantandBiologyRecognition.DAL.Payload.Respond.SavedSample;
using PlantandBiologyRecognition.DAL.Repositories;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class SavedSampleService : BaseService<SampleImageService>, ISavedSampleService
    {
        public SavedSampleService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<SavedSampleService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateSavedSampleRespond> CreateSavedSample(CreateSavedSampleRequest request)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var savedSample = _mapper.Map<Savedsample>(request);
                    await _unitOfWork.GetRepository<Savedsample>().InsertAsync(savedSample);
                    return _mapper.Map<CreateSavedSampleRespond>(savedSample);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating saved sample", ex);
            }
        }

        public async Task<GetSavedSampleRespond> GetSavedSampleById(Guid id)
        {
            var savedSample = await _unitOfWork.GetRepository<Savedsample>().GetByIdAsync(id);
            if (savedSample == null)
                throw new NotFoundException("Saved sample not found");
            return _mapper.Map<GetSavedSampleRespond>(savedSample);
        }

        public async Task<List<GetSavedSampleRespond>> GetAllSavedSamples()
        {
            var savedSamples = await _unitOfWork.GetRepository<Savedsample>().GetListAsync();
            return savedSamples.Select(ss => _mapper.Map<GetSavedSampleRespond>(ss)).ToList();
        }

        public async Task<UpdateSavedSampleRespond> UpdateSavedSample(UpdateSavedSampleRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Savedsample>();
                var savedSample = await repo.GetByIdAsync(request.Id);
                if (savedSample == null)
                    throw new NotFoundException("Saved sample not found");

                _mapper.Map(request, savedSample);
                repo.UpdateAsync(savedSample);
                return _mapper.Map<UpdateSavedSampleRespond>(savedSample);
            });
        }

        public async Task<DeleteSavedSampleRespond> DeleteSavedSample(DeleteSavedSampleRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Savedsample>();
                var savedSample = await repo.GetByIdAsync(request.Id);
                if (savedSample == null)
                    throw new NotFoundException("Saved sample not found");

                repo.DeleteAsync(savedSample);
                return _mapper.Map<DeleteSavedSampleRespond>(savedSample);
            });
        }
    }
}