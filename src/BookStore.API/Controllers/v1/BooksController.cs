using BookStore.Application.DTOs.Books.Requests;
using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class BooksController : ApiBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBooksOnly(
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

            ErrorOr<PagedResult<BookOnlyResponse>> result = await getBooksUseCase.ExecuteAsync(request, cancellationToken);
            return OkOrBadRequest(result);
        }

        [HttpGet("authors-fetched")]
        public async Task<IActionResult> GetAllBooksWithAuthorsFetched(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string? filter,
            [FromQuery] string? orderBy,
            [FromServices] IGetBooksWithAuthorsFetchedUseCase getBooksUseCase,
            CancellationToken cancellationToken)
        {
            QueryRequest request = new()
            {
                Page = page,
                PageSize = pageSize,
                Filter = filter,
                OrderBy = orderBy
            };

            ErrorOr<PagedResult<BookWithAuthorResponse>> result = await getBooksUseCase.ExecuteAsync(request, cancellationToken);
            return OkOrBadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(
            [FromRoute] int id,
            [FromServices] IGetBookByIdUseCase getBookById,
            CancellationToken cancellationToken)
        {
            BookId bookId = new(id);
            ErrorOr<BookWithAuthorResponse> result = await getBookById.ExecuteAsync(bookId, cancellationToken);
            return OkOrBadRequest(result);
        }
        
        [HttpGet("count")]
        public async Task<IActionResult> GetBooksCount(
            [FromServices] IGetBooksCountUseCase getBooksCountUseCase,
            CancellationToken cancellationToken)
        {
            ErrorOr<long> result = await getBooksCountUseCase.ExecuteAsync(GetBooksCountRequest.Instance, cancellationToken);
            return OkOrBadRequest(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddBook(
            [FromBody] AddBookRequest request,
            [FromServices] IAddBookUseCase addBookUseCase,
            CancellationToken cancellationToken)
        {
            ErrorOr<AddBookResponse> result = await addBookUseCase.ExecuteAsync(request, cancellationToken);
            return OkOrBadRequest(result);
        }
        
        [HttpPatch]
        public async Task<IActionResult> UpdateBook(
            [FromBody] UpdateBookRequest request,
            [FromServices] IUpdateBookUseCase updateBookUseCase,
            CancellationToken cancellationToken)
        {
            ErrorOr<UpdateBookResponse> result = await updateBookUseCase.ExecuteAsync(request, cancellationToken);
            return NoContentOrBadRequest(result);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(
            [FromRoute] int id,
            [FromServices] IDeleteBookUseCase deleteBookUseCase,
            CancellationToken cancellationToken)
        {
            BookId bookId = new(id);
            ErrorOr<DeleteBookResponse> result = await deleteBookUseCase.ExecuteAsync(bookId, cancellationToken);
            return NoContentOrBadRequest(result);
        }
    }
}
