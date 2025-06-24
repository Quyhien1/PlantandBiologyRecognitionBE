using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserRespond> CreateUser(CreateUserRequest createUserRequest);
        Task<CreateUserRespond> GetUserById(Guid userId);
        Task<IPaginate<CreateUserRespond>> GetAllUsers(int page = 1, int size = 10, string searchTerm = null);
        Task<bool> DeleteUser(Guid userId);
        Task<UpdateUserRespond> UpdateUser(Guid userId, UpdateUserRequest updateUserRequest);
    }
}
