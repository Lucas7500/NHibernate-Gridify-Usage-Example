using Asp.Versioning;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class ApiBase : ControllerBase
    {
        protected IActionResult OkOrBadRequest<T>(ErrorOr<T> result)
        {
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        
        protected IActionResult NoContentOrBadRequest<T>(ErrorOr<T> result)
        {
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
