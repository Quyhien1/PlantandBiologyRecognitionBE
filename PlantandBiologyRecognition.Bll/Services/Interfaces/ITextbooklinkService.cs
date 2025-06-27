using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.TextbookLink;
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
        Task<GetTextbooklinkRespond> GetTextbooklinkById(Guid id);
        Task<IPaginate<GetTextbooklinkRespond>> GetAllTextbooklinks(int page = 1, int size = 10, string searchTerm = null);
        Task<UpdateTextbooklinkRespond> UpdateTextbooklink(UpdateTextbooklinkRequest request);
        Task<DeleteTextbooklinkRespond> DeleteTextbooklink(DeleteTextbooklinkRequest request);
    }
}
