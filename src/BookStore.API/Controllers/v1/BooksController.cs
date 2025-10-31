using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { Data = "Response" });
        }
    }
}
