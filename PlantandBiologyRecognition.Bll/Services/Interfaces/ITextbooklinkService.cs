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
        /// <summary>
/// Creates a new textbook link using the provided request data.
/// </summary>
/// <param name="request">The details required to create the textbook link.</param>
/// <returns>A response containing the result of the creation operation.</returns>
Task<CreateTextbooklinkRespond> CreateTextbooklink(CreateTextbooklinkRequest request);
        /// <summary>
/// Retrieves a textbook link by its unique identifier.
/// </summary>
/// <param name="id">The unique identifier of the textbook link to retrieve.</param>
/// <returns>A task that represents the asynchronous operation, containing the textbook link response if found.</returns>
Task<GetTextbooklinkRespond> GetTextbooklinkById(Guid id);
        /// <summary>
/// Retrieves a paginated list of textbook links, optionally filtered by a search term.
/// </summary>
/// <param name="page">The page number to retrieve. Defaults to 1.</param>
/// <param name="size">The number of items per page. Defaults to 10.</param>
/// <param name="searchTerm">An optional term to filter textbook links by relevant fields.</param>
/// <returns>A paginated collection of textbook link response objects.</returns>
Task<IPaginate<GetTextbooklinkRespond>> GetAllTextbooklinks(int page = 1, int size = 10, string searchTerm = null);
        /// <summary>
/// Updates an existing textbook link with the provided information.
/// </summary>
/// <param name="request">The update request containing the new textbook link details.</param>
/// <returns>A response object with the result of the update operation.</returns>
Task<UpdateTextbooklinkRespond> UpdateTextbooklink(UpdateTextbooklinkRequest request);
        /// <summary>
/// Deletes a textbook link as specified in the request.
/// </summary>
/// <param name="request">The request containing details of the textbook link to delete.</param>
/// <returns>A response indicating the result of the deletion operation.</returns>
Task<DeleteTextbooklinkRespond> DeleteTextbooklink(DeleteTextbooklinkRequest request);
    }
}
