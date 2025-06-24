using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.TextBooLink;
using PlantandBiologyRecognition.DAL.Payload.Respond.TextbookLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface ITextbooklinkService
    {
        Task<CreateTextbooklinkRespond> CreateTextbooklink(CreateTextbooklinkRequest request);
        Task<UpdateTextbooklinkRespond> UpdateTextbooklink(UpdateTextbooklinkRequest request);
        Task<DeleteTextbooklinkRespond> DeleteTextbooklink(DeleteTextbooklinkRequest request);
        Task<GetTextbooklinkRespond> GetTextbooklinkById(Guid id);
        Task<List<GetTextbooklinkRespond>> GetAllTextbooklinks();
    }
}
