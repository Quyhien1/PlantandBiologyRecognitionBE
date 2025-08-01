﻿using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.TextbookLink;
using PlantandBiologyRecognition.DAL.Payload.Respond.TextbookLink;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class TextbooklinkService : BaseService<TextbooklinkService>, ITextbooklinkService
    {
        public TextbooklinkService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<TextbooklinkService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateTextbooklinkRespond> CreateTextbooklink(CreateTextbooklinkRequest request)
        {
            try
            {
                return await _unitOfWork.ProcessInTransactionAsync(async () =>
                {
                    var entity = _mapper.Map<Textbooklink>(request);
                    entity.LinkId = Guid.NewGuid();
                    await _unitOfWork.GetRepository<Textbooklink>().InsertAsync(entity);
                    return _mapper.Map<CreateTextbooklinkRespond>(entity);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating textbooklink: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<UpdateTextbooklinkRespond> UpdateTextbooklink(UpdateTextbooklinkRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Textbooklink>();
                var entity = await repo.GetByIdAsync(request.LinkId);
                if (entity == null)
                    throw new NotFoundException("Textbooklink not found");

                _mapper.Map(request, entity);
                 repo.UpdateAsync(entity);
                return _mapper.Map<UpdateTextbooklinkRespond>(entity);
            });
        }

        public async Task<DeleteTextbooklinkRespond> DeleteTextbooklink(DeleteTextbooklinkRequest request)
        {
            return await _unitOfWork.ProcessInTransactionAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Textbooklink>();
                var entity = await repo.GetByIdAsync(request.LinkId);
                if (entity == null)
                    throw new NotFoundException("Textbooklink not found");

                 repo.DeleteAsync(entity);
                return new DeleteTextbooklinkRespond
                {
                    Success = true,
                    Message = $"Textbooklink deleted successfully. Reason: {request.Reason}"
                };
            });
        }

        public async Task<GetTextbooklinkRespond> GetTextbooklinkById(Guid id)
        {
            var entity = await _unitOfWork.GetRepository<Textbooklink>().SingleOrDefaultAsync(
                predicate: x => x.SampleId == id
            );
            
            if (entity == null)
                throw new NotFoundException("Textbooklink not found for this sample");

            return _mapper.Map<GetTextbooklinkRespond>(entity);
        }

        public async Task<IPaginate<GetTextbooklinkRespond>> GetAllTextbooklinks(int page = 1, int size = 10, string searchTerm = null)
        {
            try
            {
                string searchTermLower = searchTerm?.ToLower();

                return await _unitOfWork.GetRepository<Textbooklink>().GetPagingListAsync(
                    selector: x => _mapper.Map<GetTextbooklinkRespond>(x),
                    predicate: x => string.IsNullOrWhiteSpace(searchTerm) ||
                                   (x.TextbookName.ToLower().Contains(searchTermLower) ||
                                    x.Lesson.ToLower().Contains(searchTermLower)),
                    orderBy: q => q.OrderByDescending(x => x.LinkId),
                    page: page,
                    size: size
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all textbook links: {Message}", ex.Message);
                throw;
            }
        }
    }
}
