using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    public abstract class ApiBase : ControllerBase
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
