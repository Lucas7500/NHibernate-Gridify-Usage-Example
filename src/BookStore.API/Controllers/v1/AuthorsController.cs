using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { Data = "Response" });
        }
    }
}
