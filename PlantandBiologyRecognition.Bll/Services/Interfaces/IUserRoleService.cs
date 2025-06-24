using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.UserRole;
using PlantandBiologyRecognition.DAL.Payload.Respond.UserRole;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IUserRoleService
    {
        /// <summary>
/// Asynchronously creates a new user role using the provided request data.
/// </summary>
/// <param name="createUserRoleRequest">The data required to create the user role.</param>
/// <returns>A task that represents the asynchronous operation, containing the created user role information.</returns>
Task<UserRoleRespond> CreateUserRole(CreateUserRoleRequest createUserRoleRequest);
        /// <summary>
/// Retrieves a user role by its unique identifier.
/// </summary>
/// <param name="roleId">The unique identifier of the user role to retrieve.</param>
/// <returns>A task that represents the asynchronous operation. The task result contains the user role information if found.</returns>
Task<UserRoleRespond> GetUserRoleById(Guid roleId);
        /// <summary>
/// Retrieves a paginated list of user roles, optionally filtered by a search term.
/// </summary>
/// <param name="page">The page number to retrieve. Defaults to 1.</param>
/// <param name="size">The number of user roles per page. Defaults to 10.</param>
/// <param name="searchTerm">An optional term to filter user roles by name or description.</param>
/// <returns>A paginated collection of user roles matching the specified criteria.</returns>
Task<IPaginate<UserRoleRespond>> GetAllUserRoles(int page = 1, int size = 10, string searchTerm = null);
        /// <summary>
/// Retrieves all user roles associated with the specified user.
/// </summary>
/// <param name="userId">The unique identifier of the user whose roles are to be retrieved.</param>
/// <returns>An enumerable collection of user roles assigned to the user.</returns>
Task<IEnumerable<UserRoleRespond>> GetUserRolesByUserId(Guid userId);
        /// <summary>
/// Deletes the user role identified by the specified role ID.
/// </summary>
/// <param name="roleId">The unique identifier of the user role to delete.</param>
/// <returns>True if the user role was successfully deleted; otherwise, false.</returns>
Task<bool> DeleteUserRole(Guid roleId);
        /// <summary>
/// Updates an existing user role with the specified data.
/// </summary>
/// <param name="roleId">The unique identifier of the user role to update.</param>
/// <param name="updateUserRoleRequest">The data used to update the user role.</param>
/// <returns>The updated user role information.</returns>
Task<UserRoleRespond> UpdateUserRole(Guid roleId, UpdateUserRoleRequest updateUserRoleRequest);
    }
}
