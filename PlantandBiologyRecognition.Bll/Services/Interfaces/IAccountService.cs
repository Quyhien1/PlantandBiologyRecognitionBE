using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Payload.Request.Account;
using PlantandBiologyRecognition.DAL.Payload.Respond.Account;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<CreateAccountRespond> CreateAccount(CreateAccountRequest createAccountRequest);
    }
}
