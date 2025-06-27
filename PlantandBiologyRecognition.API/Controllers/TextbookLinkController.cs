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

        public TextbooklinkController(ITextbooklinkService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string searchTerm = null)
        {
            return Ok(await _service.GetAllTextbooklinks(page, size, searchTerm));
        }

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
