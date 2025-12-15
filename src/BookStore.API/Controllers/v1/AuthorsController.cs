using BookStore.Application.DTOs.Authors.Requests;
using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Application.UseCases.Authors.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorsController : ApiBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string? filter,
            [FromQuery] string? orderBy,
            [FromServices] IGetAuthorsUseCase getAuthorsUseCase,
            CancellationToken cancellationToken)
        {
            QueryRequest request = new()
            {
                Page = page,
                PageSize = pageSize,
                Filter = filter,
                OrderBy = orderBy
            };

            ErrorOr<PagedResult<AuthorResponse>> result = await getAuthorsUseCase.ExecuteAsync(request, cancellationToken);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }

        [HttpGet("/{id:guid}")]
        public async Task<IActionResult> GetAuthorById(
            [FromRoute] Guid id,
            [FromServices] IGetAuthorByIdUseCase getAuthorById,
            CancellationToken cancellationToken)
        {
            AuthorId authorId = new(id);
            ErrorOr<AuthorResponse> result = await getAuthorById.ExecuteAsync(authorId, cancellationToken);
            return OkOrBadRequest(result);
        }

        [HttpGet("/count")]
        public async Task<IActionResult> GetAuthorsCount(
            [FromServices] IGetAuthorsCountUseCase getAuthorsCountUseCase,
            CancellationToken cancellationToken)
        {
            ErrorOr<long> result = await getAuthorsCountUseCase.ExecuteAsync(GetAuthorsCountRequest.Instance, cancellationToken);
            return OkOrBadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(
            [FromBody] AddAuthorRequest request,
            [FromServices] IAddAuthorUseCase addAuthorUseCase,
            CancellationToken cancellationToken)
        {
            ErrorOr<AddAuthorResponse> result = await addAuthorUseCase.ExecuteAsync(request, cancellationToken);

            return OkOrBadRequest(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAuthor(
            [FromBody] UpdateAuthorRequest request,
            [FromServices] IUpdateAuthorUseCase updateAuthorUseCase,
            CancellationToken cancellationToken)
        {
            ErrorOr<UpdateAuthorResponse> result = await updateAuthorUseCase.ExecuteAsync(request, cancellationToken);
            return NoContentOrBadRequest(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAuthor(
            [FromRoute] Guid id,
            [FromServices] IDeleteAuthorUseCase deleteAuthorUseCase,
            CancellationToken cancellationToken)
        {
            AuthorId authorId = new(id);
            ErrorOr<DeleteAuthorResponse> result = await deleteAuthorUseCase.ExecuteAsync(authorId, cancellationToken);
            return NoContentOrBadRequest(result);
        }
    }
}
