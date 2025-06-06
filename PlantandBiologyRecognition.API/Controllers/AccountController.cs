﻿using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.API.Constants;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Payload.Request.Account;
using PlantandBiologyRecognition.DAL.Payload.Respond.Account;

namespace PlantandBiologyRecognition.API.Controllers
{
    [ApiController]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
            : base(logger)
        {
            _accountService = accountService;
        }
        [HttpPost(ApiEndPointConstant.Account.CreateAccount)]
        [ProducesResponseType(typeof(CreateAccountRespond), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest createAccountRequest)
        {
            if (createAccountRequest == null)
            {
                return BadRequest("Invalid account creation request.");
            }
            try
            {
                var response = await _accountService.CreateAccount(createAccountRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the account.");
            }
        }
    }
}
