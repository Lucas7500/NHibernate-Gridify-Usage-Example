using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class GetBooksUseCase(IBooksQueryService booksQueryService) : IGetBooksUseCase
    {
        public async Task<PagedResult<BookOnlyResponse>> ExecuteAsync(QueryRequest request, CancellationToken cancellationToken = default)
        {
            return await booksQueryService.GetAllAsync(request, cancellationToken);
        }
    }
}
