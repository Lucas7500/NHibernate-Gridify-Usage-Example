using Asp.Versioning;
using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page, 
            [FromQuery] int pageSize, 
            [FromQuery] string? filter,
            [FromQuery] string? orderBy,
            [FromServices] IGetBooksUseCase getBooksUseCase,
            CancellationToken cancellationToken)
        {
            QueryRequest request = new()
            {
                Page = page,
                PageSize = pageSize,
                Filter = filter,
                OrderBy = orderBy
            };

            PagedResult<BookOnlyResponse> result = await getBooksUseCase.ExecuteAsync(request, cancellationToken);

            return Ok(result);
        }
    }
}
