using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Sample;
using PlantandBiologyRecognition.DAL.Payload.Respond.Sample;
using PlantandBiologyRecognition.DAL.Repositories;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class SampleService : BaseService<SampleService>, ISampleService
    {
        public SampleService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<SampleService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateSampleRespond> CreateSample(CreateSampleRequest request)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var sample = _mapper.Map<Sample>(request);
                    await _unitOfWork.GetRepository<Sample>().InsertAsync(sample);
                    return _mapper.Map<CreateSampleRespond>(sample);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating sample", ex);
            }
        }

        public async Task<GetSampleRespond> GetSampleById(Guid id)
        {
            var sample = await _unitOfWork.GetRepository<Sample>().GetByIdAsync(id);
            if (sample == null)
                throw new NotFoundException("Sample not found");
            return _mapper.Map<GetSampleRespond>(sample);
        }

        public async Task<GetSampleRespond> GetSampleByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Sample name cannot be empty");
                
            var sample = await _unitOfWork.GetRepository<Sample>().SingleOrDefaultAsync(
                predicate: x => x.Name.ToLower() == name.ToLower()
            );
                
            if (sample == null)
                throw new NotFoundException($"Sample with name '{name}' not found");
                
            return _mapper.Map<GetSampleRespond>(sample);
        }

        public async Task<IPaginate<GetSampleRespond>> GetAllSamples(int page = 1, int size = 10, string searchTerm = null)
        {
            try
            {
                string searchTermLower = searchTerm?.ToLower();
                return await _unitOfWork.GetRepository<Sample>().GetPagingListAsync(
                    selector: x => _mapper.Map<GetSampleRespond>(x),
                    predicate: x => string.IsNullOrWhiteSpace(searchTerm) || x.Name.ToLower().Contains(searchTermLower),
                    orderBy: q => q.OrderByDescending(x => x.SampleId),
                    page: page,
                    size: size
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated samples: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<UpdateSampleRespond> UpdateSample(UpdateSampleRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Sample>();
                var sample = await repo.GetByIdAsync(request.Id);
                if (sample == null)
                    throw new NotFoundException("Sample not found");
                if (!string.IsNullOrEmpty(request.Name))
                    sample.Name = request.Name;
                if (!string.IsNullOrEmpty(request.ScientificName))
                    sample.ScientificName = request.ScientificName;
                if (request.CategoryId.HasValue)
                    sample.CategoryId = request.CategoryId;

                repo.UpdateAsync(sample);
                return _mapper.Map<UpdateSampleRespond>(sample);
            });
        }

        public async Task<DeleteSampleRespond> DeleteSample(DeleteSampleRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Sample>();
                var sample = await repo.GetByIdAsync(request.Id);
                if (sample == null)
                    throw new NotFoundException("Sample not found");

                repo.DeleteAsync(sample);
                return _mapper.Map<DeleteSampleRespond>(sample);
            });
        }
    }
}