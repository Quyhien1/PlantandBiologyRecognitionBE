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
        /// <summary>
/// Asynchronously creates a new user with the specified details.
/// </summary>
/// <param name="createUserRequest">The request containing user information to be created.</param>
/// <returns>A task that resolves to a response containing the created user's details.</returns>
Task<CreateUserRespond> CreateUser(CreateUserRequest createUserRequest);
        /// <summary>
/// Asynchronously retrieves user details by their unique identifier.
/// </summary>
/// <param name="userId">The unique identifier of the user to retrieve.</param>
/// <returns>A task that resolves to the user's response data if found; otherwise, null.</returns>
Task<CreateUserRespond> GetUserById(Guid userId);
        /// <summary>
/// Retrieves a paginated list of users, optionally filtered by a search term.
/// </summary>
/// <param name="page">The page number to retrieve. Defaults to 1.</param>
/// <param name="size">The number of users per page. Defaults to 10.</param>
/// <param name="searchTerm">An optional term to filter users by name or other criteria.</param>
/// <returns>A task that resolves to a paginated collection of user response objects.</returns>
Task<IPaginate<CreateUserRespond>> GetAllUsers(int page = 1, int size = 10, string searchTerm = null);
        /// <summary>
/// Deletes the user with the specified unique identifier.
/// </summary>
/// <param name="userId">The unique identifier of the user to delete.</param>
/// <returns>A task that resolves to true if the user was successfully deleted; otherwise, false.</returns>
Task<bool> DeleteUser(Guid userId);
        /// <summary>
/// Updates the information of an existing user identified by the specified user ID.
/// </summary>
/// <param name="userId">The unique identifier of the user to update.</param>
/// <param name="updateUserRequest">The request object containing updated user information.</param>
/// <returns>A task that resolves to a response containing the updated user details.</returns>
Task<UpdateUserRespond> UpdateUser(Guid userId, UpdateUserRequest updateUserRequest);
    }
}
