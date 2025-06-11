using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.MetaDatas;
using PlantandBiologyRecognition.DAL.Payload.Request.User;
using PlantandBiologyRecognition.DAL.Payload.Respond.User;

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
    
            [HttpPost(ApiEndPointConstant.User.UserEndPoint)]
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
    }
}
