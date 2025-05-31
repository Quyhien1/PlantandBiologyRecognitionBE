using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request;
using PlantandBiologyRecognition.DAL.Payload.Respond;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

namespace PlantandBiologyRecognition.BLL.Services.Implements
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        public AccountService(IUnitOfWork<AppDbContext> unitOfWork, ILogger<AccountService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
        public async Task<CreateAccountRespond> CreateAccount(CreateAccountRequest createAccountRequest)
        {
            var existingAccount = await _unitOfWork.GetRepository<Account>()
                        .SingleOrDefaultAsync(predicate: s => s.Email == createAccountRequest.Email);
            if (existingAccount != null)
            {
                throw new InvalidOperationException("Email is already in use. Please use a different email.");
            }
            var newAccount = _mapper.Map<Account>(createAccountRequest);
            newAccount.Accountid = Guid.NewGuid();
            newAccount.Password = createAccountRequest.Password;
            newAccount.Isactive = true;
            newAccount.Phone = null;
            await _unitOfWork.GetRepository<Account>()
                .InsertAsync(newAccount);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CreateAccountRespond>(newAccount);
        }
    }
}

