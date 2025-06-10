using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class UserService : BaseService<UserService>, IUserService
    {
        public UserService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
       //public async Task<CreateUserRespond> CreateUser(CreateUserRequest createUserRequest)
       // {
       //     if (createUserRequest == null)
       //     {
       //         throw new ArgumentNullException(nameof(createUserRequest), "Create user request cannot be null.");
       //     }
       //     var user = _mapper.Map<User>(createUserRequest);
       //     user.UserId = Guid.NewGuid();
       //     user.CreatedAt = DateTime.UtcNow;
       //     if (createUserRequest.Avatar != null)
       //     {
       //         // Handle avatar upload logic here, e.g., save to file system or cloud storage
       //         user.Avatar = await SaveAvatarAsync(createUserRequest.Avatar);
       //     }
       //     _unitOfWork.UserRepository.Add(user);
       //     await _unitOfWork.CommitAsync();
       //     return _mapper.Map<CreateUserRespond>(user);
       // }
        
    }
}
