using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.API.Validators;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;
using static PlantandBiologyRecognition.API.Constants.ApiEndPointConstant;

namespace PlantandBiologyRecognition.API.Controllers
{
    public class UserController : BaseController<UserController>
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
            : base(logger)
        {
            _userService = userService;
        }
        
        [HttpPost(ApiEndPointConstant.Users.UsersEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<CreateUserRespond>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserRequest createUserRequest)
        {

            var response = await _userService.CreateUser(createUserRequest);
            if (response == null)
            {
                return BadRequest(
                    ApiResponseBuilder.BuildErrorResponse<object>(
                        null,
                        StatusCodes.Status400BadRequest,
                        "Failed to create staff",
                        "The staff creation process failed"
                    )
                );
            }
            return CreatedAtAction(
                nameof(CreateUser),
                ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status200OK,
                    "User created successfully",
                    response
                )
            );
        }
        [CustomAuthorize(RoleName.Admin)]
        [HttpGet(ApiEndPointConstant.Users.GetUserByIdEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<CreateUserRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {

            var user = await _userService.GetUserById(userId);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User retrieved successfully",
                user
            ));
        }
        [CustomAuthorize(RoleName.Admin)]
        [HttpGet(ApiEndPointConstant.Users.UsersEndPoint)]
        [ProducesResponseType(typeof(ApiResponse<IPaginate<CreateUserRespond>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            var users = await _userService.GetAllUsers(page, size, searchTerm);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "Users retrieved successfully",
                users
            ));
        }
        [HttpDelete(ApiEndPointConstant.Users.DeleteUserEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var isDeleted = await _userService.DeleteUser(userId);
            if (!isDeleted)
            {
                return NotFound(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status404NotFound,
                    "User not found",
                    "The user you are trying to delete does not exist."
                ));
            }
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User deleted successfully",
                isDeleted
            ));
        }
        [CustomAuthorize(RoleName.Admin, RoleName.Student, RoleName.Teacher)]
        [HttpPut(ApiEndPointConstant.Users.UpdateUserEndpoint)]
        [ProducesResponseType(typeof(ApiResponse<UpdateUserRespond>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromForm] UpdateUserRequest updateUserRequest)
        {
            var updatedUser = await _userService.UpdateUser(userId, updateUserRequest);
            return Ok(ApiResponseBuilder.BuildResponse(
                StatusCodes.Status200OK,
                "User updated successfully",
                updatedUser
            ));
        }
    }
}