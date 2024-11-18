using DomainProperty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DomainProperty.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties([FromQuery] int page =0 , [FromQuery] int pageSize = 10)
        {

            var properties = await _service.GetAllPropertiesAsync(page , pageSize);
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await _service.GetPropertyByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }
    }
}
