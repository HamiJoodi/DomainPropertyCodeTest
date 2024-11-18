using DomainProperty.Services;
using Microsoft.AspNetCore.Mvc;

namespace DomainProperty.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuburbController : ControllerBase
    {
        private readonly IPropertyService _service;

        public SuburbController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuburbs()
        {
            var suburbs = await _service.GetSuburbAverageAsync();
            return Ok(suburbs);
        }
    }
}
