using PlantandBiologyRecognition.DAL.Payload.Request.UserRole;
using PlantandBiologyRecognition.DAL.Payload.Respond.UserRole;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleRespond> CreateUserRole(CreateUserRoleRequest createUserRoleRequest);
        Task<UserRoleRespond> GetUserRoleById(Guid roleId);
        Task<IEnumerable<UserRoleRespond>> GetAllUserRoles();
        Task<IEnumerable<UserRoleRespond>> GetUserRolesByUserId(Guid userId);
        Task<bool> DeleteUserRole(Guid roleId);
        Task<UserRoleRespond> UpdateUserRole(Guid roleId, UpdateUserRoleRequest updateUserRoleRequest);
    }
}
