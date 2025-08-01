using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.SampleDetail;
using PlantandBiologyRecognition.DAL.Payload.Respond.SampleDetail;
using PlantandBiologyRecognition.DAL.Repositories;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class SampleDetailService : BaseService<SampleDetailService>, ISampleDetailService
    {
        public SampleDetailService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<SampleDetailService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateSampleDetailRespond> CreateSampleDetail(CreateSampleDetailRequest request)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var sampleDetail = _mapper.Map<Sampledetail>(request);
                    await _unitOfWork.GetRepository<Sampledetail>().InsertAsync(sampleDetail);
                    return _mapper.Map<CreateSampleDetailRespond>(sampleDetail);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating sample detail", ex);
            }
        }

        public async Task<GetSampleDetailRespond> GetSampleDetailById(Guid id)
        {
            var sampleDetail = await _unitOfWork.GetRepository<Sampledetail>().GetByIdAsync(id);
            if (sampleDetail == null)
                throw new NotFoundException("Sample detail not found");
            return _mapper.Map<GetSampleDetailRespond>(sampleDetail);
        }

        public async Task<IPaginate<GetSampleDetailRespond>> GetAllSampleDetails(int page = 1, int size = 10, string searchTerm = null)
        {
            try
            {
                string searchTermLower = searchTerm?.ToLower();
                return await _unitOfWork.GetRepository<Sampledetail>().GetPagingListAsync(
                    selector: x => _mapper.Map<GetSampleDetailRespond>(x),
                    predicate: x => string.IsNullOrWhiteSpace(searchTerm) || x.Description.ToLower().Contains(searchTermLower),
                    orderBy: q => q.OrderByDescending(x => x.DetailId),
                    page: page,
                    size: size
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated sample details: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<UpdateSampleDetailRespond> UpdateSampleDetail(UpdateSampleDetailRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Sampledetail>();
                var sampleDetail = await repo.GetByIdAsync(request.Id);
                if (sampleDetail == null)
                    throw new NotFoundException("Sample detail not found");
                if (!string.IsNullOrEmpty(request.Description))
                    sampleDetail.Description = request.Description;
                if (!string.IsNullOrEmpty(request.Habitat))
                    sampleDetail.Habitat = request.Habitat;
                if (!string.IsNullOrEmpty(request.Behavior))
                    sampleDetail.Behavior = request.Behavior;
                if (!string.IsNullOrEmpty(request.OtherInfo))
                    sampleDetail.OtherInfo = request.OtherInfo;

                repo.UpdateAsync(sampleDetail);
                return _mapper.Map<UpdateSampleDetailRespond>(sampleDetail);
            });
        }

        public async Task<DeleteSampleDetailRespond> DeleteSampleDetail(DeleteSampleDetailRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Sampledetail>();
                var sampleDetail = await repo.GetByIdAsync(request.Id);
                if (sampleDetail == null)
                    throw new NotFoundException("Sample detail not found");

                repo.DeleteAsync(sampleDetail);
                return _mapper.Map<DeleteSampleDetailRespond>(sampleDetail);
            });
        }
    }
}