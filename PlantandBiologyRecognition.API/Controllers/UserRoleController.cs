using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Payload.Request.UserRole;
using PlantandBiologyRecognition.DAL.Payload.Respond.UserRole;
using System;
using System.Threading.Tasks;
using static PlantandBiologyRecognition.API.Constants.ApiEndPointConstant;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class UserRoleController : BaseController<UserRoleController>
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService, ILogger<UserRoleController> logger)
            : base(logger)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost(ApiEndPointConstant.UserRoles.UserRolesEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<UserRoleRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleRequest createUserRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status400BadRequest,
                    "Invalid role data provided.",
                    ModelState
                ));
            }

            var response = await _userRoleService.CreateUserRole(createUserRoleRequest);
            return CreatedAtAction(
                nameof(CreateUserRole),
                ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status201Created,
                    "User role created successfully",
                    response
                )
            );
        }

        [HttpGet(ApiEndPointConstant.UserRoles.GetUserRoleByIdEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<UserRoleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserRoleById(Guid roleId)
        {
            var userRole = await _userRoleService.GetUserRoleById(roleId);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User role retrieved successfully",
                userRole
            ));
        }

        [HttpGet(ApiEndPointConstant.UserRoles.UserRolesEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserRoleRespond>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserRoles()
        {
            var userRoles = await _userRoleService.GetAllUserRoles();
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User roles retrieved successfully",
                userRoles
            ));
        }

        [HttpGet(ApiEndPointConstant.UserRoles.GetUserRolesByUserIdEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserRoleRespond>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserRolesByUserId(Guid userId)
        {
            var userRoles = await _userRoleService.GetUserRolesByUserId(userId);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User roles retrieved successfully",
                userRoles
            ));
        }

        [HttpDelete(ApiEndPointConstant.UserRoles.DeleteUserRoleEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserRole(Guid roleId)
        {
            var isDeleted = await _userRoleService.DeleteUserRole(roleId);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User role deleted successfully",
                isDeleted
            ));
        }

        [HttpPut(ApiEndPointConstant.UserRoles.UpdateUserRoleEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<UserRoleRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserRole(Guid roleId, [FromBody] UpdateUserRoleRequest updateUserRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status400BadRequest,
                    "Invalid role data provided.",
                    ModelState
                ));
            }

            var updatedUserRole = await _userRoleService.UpdateUserRole(roleId, updateUserRoleRequest);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User role updated successfully",
                updatedUserRole
            ));
        }
    }
}
