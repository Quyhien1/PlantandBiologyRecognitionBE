using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.UserRole;
using PlantandBiologyRecognition.DAL.Payload.Respond.UserRole;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PlantandBiologyRecognition.DAL.Paginate;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class UserRoleService : BaseService<UserRoleService>, IUserRoleService
    {
        public UserRoleService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<UserRoleService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<UserRoleRespond> CreateUserRole(CreateUserRoleRequest createUserRoleRequest)
        {
            try
            {
                if (createUserRoleRequest == null)
                {
                    throw new ArgumentNullException(nameof(createUserRoleRequest), "Create user role request cannot be null.");
                }

                // Validate that the role name is defined in the enum
                if (!Enum.IsDefined(typeof(RoleName), createUserRoleRequest.RoleName))
                {
                    throw new BusinessException($"Invalid role name: {createUserRoleRequest.RoleName}. Role must be one of the predefined system roles.");
                }

                // Check if user exists
                var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                    predicate: s => s.UserId == createUserRoleRequest.UserId && s.IsActive);
                if (user == null)
                {
                    throw new NotFoundException($"User with ID {createUserRoleRequest.UserId} not found.");
                }

                // Check if role already exists for the user
                var existingRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.UserId == createUserRoleRequest.UserId && 
                                   r.RoleName == createUserRoleRequest.RoleName.ToString());
                if (existingRole != null)
                {
                    throw new BusinessException($"User already has the role {createUserRoleRequest.RoleName}.");
                }

                var newUserRole = _mapper.Map<Userrole>(createUserRoleRequest);
                newUserRole.RoleId = Guid.NewGuid();
                
                await _unitOfWork.GetRepository<Userrole>().InsertAsync(newUserRole);
                await _unitOfWork.CommitAsync();

                // Fetch the complete user role with user data
                var completeUserRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.RoleId == newUserRole.RoleId,
                    include: i => i.Include(x => x.User));

                return _mapper.Map<UserRoleRespond>(completeUserRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user role: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a user role by its unique identifier, including associated user information.
        /// </summary>
        /// <param name="roleId">The unique identifier of the user role to retrieve.</param>
        /// <returns>The user role response corresponding to the specified role ID.</returns>
        public async Task<UserRoleRespond> GetUserRoleById(Guid roleId)
        {
            try
            {
                if (roleId == Guid.Empty)
                {
                    throw new ArgumentException("Role ID cannot be empty.", nameof(roleId));
                }

                var userRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.RoleId == roleId,
                    include: i => i.Include(x => x.User));

                if (userRole == null)
                {
                    throw new NotFoundException($"User role with ID {roleId} not found.");
                }

                return _mapper.Map<UserRoleRespond>(userRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user role by ID: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a paginated list of user roles, optionally filtered by a search term in the role name.
        /// </summary>
        /// <param name="page">The page number to retrieve. Defaults to 1.</param>
        /// <param name="size">The number of items per page. Defaults to 10.</param>
        /// <param name="searchTerm">An optional search term to filter user roles by role name.</param>
        /// <returns>A paginated list of user roles matching the specified criteria.</returns>
        public async Task<IPaginate<UserRoleRespond>> GetAllUserRoles(int page = 1, int size = 10, string searchTerm = null)
        {
            try
            {
                string searchTermLower = searchTerm?.ToLower();
                
                return await _unitOfWork.GetRepository<Userrole>().GetPagingListAsync(
                    selector: x => _mapper.Map<UserRoleRespond>(x),
                    predicate: x => string.IsNullOrWhiteSpace(searchTerm) || 
                                   x.RoleName.ToLower().Contains(searchTermLower),
                    page: page,
                    size: size
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user roles: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<UserRoleRespond>> GetUserRolesByUserId(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    throw new ArgumentException("User ID cannot be empty.", nameof(userId));
                }

                var userRoles = await _unitOfWork.GetRepository<Userrole>().GetListAsync(
                    predicate: r => r.UserId == userId,
                    include: i => i.Include(x => x.User));

                return _mapper.Map<IEnumerable<UserRoleRespond>>(userRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user roles by user ID: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteUserRole(Guid roleId)
        {
            try
            {
                if (roleId == Guid.Empty)
                {
                    throw new ArgumentException("Role ID cannot be empty.", nameof(roleId));
                }

                var userRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.RoleId == roleId);

                if (userRole == null)
                {
                    throw new NotFoundException($"User role with ID {roleId} not found.");
                }

                _unitOfWork.GetRepository<Userrole>().DeleteAsync(userRole);
                await _unitOfWork.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user role: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<UserRoleRespond> UpdateUserRole(Guid roleId, UpdateUserRoleRequest updateUserRoleRequest)
        {
            try
            {
                if (roleId == Guid.Empty)
                {
                    throw new ArgumentException("Role ID cannot be empty.", nameof(roleId));
                }

                if (updateUserRoleRequest == null)
                {
                    throw new ArgumentNullException(nameof(updateUserRoleRequest), "Update user role request cannot be null.");
                }

                // Validate that the role name is defined in the enum
                if (!Enum.IsDefined(typeof(RoleName), updateUserRoleRequest.RoleName))
                {
                    throw new BusinessException($"Invalid role name: {updateUserRoleRequest.RoleName}. Role must be one of the predefined system roles.");
                }

                var userRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.RoleId == roleId);

                if (userRole == null)
                {
                    throw new NotFoundException($"User role with ID {roleId} not found.");
                }

                // Check if user exists if userId is changing
                if (userRole.UserId != updateUserRoleRequest.UserId)
                {
                    var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                        predicate: s => s.UserId == updateUserRoleRequest.UserId && s.IsActive);
                    if (user == null)
                    {
                        throw new NotFoundException($"User with ID {updateUserRoleRequest.UserId} not found.");
                    }
                }

                // Check if new role already exists for the target user
                var existingRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.UserId == updateUserRoleRequest.UserId && 
                                  r.RoleName == updateUserRoleRequest.RoleName.ToString() && 
                                  r.RoleId != roleId);
                
                if (existingRole != null)
                {
                    throw new BusinessException($"User already has the role {updateUserRoleRequest.RoleName}.");
                }

                _mapper.Map(updateUserRoleRequest, userRole);
                _unitOfWork.GetRepository<Userrole>().UpdateAsync(userRole);
                await _unitOfWork.CommitAsync();

                // Fetch the updated user role with user data
                var updatedUserRole = await _unitOfWork.GetRepository<Userrole>().SingleOrDefaultAsync(
                    predicate: r => r.RoleId == roleId,
                    include: i => i.Include(x => x.User));

                return _mapper.Map<UserRoleRespond>(updatedUserRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user role: {Message}", ex.Message);
                throw;
            }
        }
    }
}
