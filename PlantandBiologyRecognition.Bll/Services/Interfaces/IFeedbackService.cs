using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantandBiologyRecognition.DAL.Paginate;
using PlantandBiologyRecognition.DAL.Payload.Request.Feedback;
using PlantandBiologyRecognition.DAL.Payload.Respond.Feedback;

namespace PlantandBiologyRecognition.BLL.Services.Interfaces
{
    public interface IFeedbackService
    {
        /// <summary>
/// Creates a new feedback entry based on the provided request data.
/// </summary>
/// <param name="createFeedbackRequest">The request containing details for the new feedback.</param>
/// <returns>A task that resolves to the response containing information about the created feedback.</returns>
Task<CreateFeedbackRespond> CreateFeedback(CreateFeedbackRequest createFeedbackRequest);
        /// <summary>
/// Retrieves the details of a feedback entry by its unique identifier.
/// </summary>
/// <param name="feedbackId">The unique identifier of the feedback to retrieve.</param>
/// <returns>A task that represents the asynchronous operation, containing the feedback details if found.</returns>
Task<GetFeedbackRespond> GetFeedbackById(Guid feedbackId);
        /// <summary>
/// Retrieves a paginated list of feedback responses, optionally filtered by a search term.
/// </summary>
/// <param name="page">The page number to retrieve. Defaults to 1.</param>
/// <param name="size">The number of feedback items per page. Defaults to 10.</param>
/// <param name="searchTerm">An optional term to filter feedback results.</param>
/// <returns>A task that resolves to a paginated collection of feedback responses.</returns>
Task<IPaginate<GetFeedbackRespond>> GetAllFeedbacks(int page = 1, int size = 10, string searchTerm = null);
        /// <summary>
/// Updates an existing feedback entry with the provided information.
/// </summary>
/// <param name="updateFeedbackRequest">The request containing updated feedback details.</param>
/// <returns>A task that resolves to the response containing the result of the update operation.</returns>
Task<UpdateFeedbackRespond> UpdateFeedback(UpdateFeedbackRequest updateFeedbackRequest);
        /// <summary>
/// Deletes feedback as specified in the provided request.
/// </summary>
/// <param name="deleteFeedbackRequest">The request containing details of the feedback to delete.</param>
/// <returns>A task that resolves to a response indicating the result of the deletion operation.</returns>
Task<DeleteFeedbackRespond> DeleteFeedback(DeleteFeedbackRequest deleteFeedbackRequest);
    }
}
