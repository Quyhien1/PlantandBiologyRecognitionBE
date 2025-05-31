using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<AppDbContext> _unitOfWork;
        protected ILogger _logger;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;
    
    public BaseService(IUnitOfWork<AppDbContext> unitOfWork, ILogger logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
