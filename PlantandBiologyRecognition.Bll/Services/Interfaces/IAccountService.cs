using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Payload.Request;
using PlantandBiologyRecognition.DAL.Payload.Respond;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<CreateAccountRespond> CreateAccount(CreateAccountRequest createAccountRequest);
    }
}
