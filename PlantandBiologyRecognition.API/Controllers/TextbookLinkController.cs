using Microsoft.AspNetCore.Mvc;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Exceptions;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Payload.Request.TextbookLink;

namespace PlantandBiologyRecognition.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextbooklinkController : ControllerBase
    {
        private readonly ITextbooklinkService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextbooklinkController"/> class with the specified textbook link service.
        /// </summary>
        public TextbooklinkController(ITextbooklinkService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a paginated list of textbook links, optionally filtered by a search term.
        /// </summary>
        /// <param name="page">The page number to retrieve. Defaults to 1.</param>
        /// <param name="size">The number of items per page. Defaults to 10.</param>
        /// <param name="searchTerm">An optional search term to filter textbook links.</param>
        /// <returns>An HTTP 200 response containing the paginated and filtered list of textbook links.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            return Ok(await _service.GetAllTextbooklinks(page, size, searchTerm));
        }

        /// <summary>
        /// Retrieves a textbook link by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the textbook link.</param>
        /// <returns>HTTP 200 with the textbook link if found; HTTP 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _service.GetTextbooklinkById(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTextbooklinkRequest request)
        {
            var result = await _service.CreateTextbooklink(request);
            return CreatedAtAction(nameof(GetById), new { id = result.LinkId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTextbooklinkRequest request)
        {
            try
            {
                var result = await _service.UpdateTextbooklink(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTextbooklinkRequest request)
        {
            try
            {
                var result = await _service.DeleteTextbooklink(request);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
