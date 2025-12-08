using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class GetBooksWithAuthorsFetchedUseCase(IBooksQueryService booksQueryService) : IGetBooksWithAuthorsFetchedUseCase
    {
        public async Task<PagedResult<BookWithAuthorResponse>> ExecuteAsync(QueryRequest request, CancellationToken cancellationToken = default)
        {
            return await booksQueryService.GetAllWithAuthorsFetchedAsync(request, cancellationToken);
        }
    }
}
