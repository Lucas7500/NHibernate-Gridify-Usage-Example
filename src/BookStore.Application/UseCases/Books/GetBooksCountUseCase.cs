using BookStore.Application.DTOs.Books.Requests;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Books.Contracts;
using ErrorOr;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class GetBooksCountUseCase(IBooksQueryService booksQueryService) : IGetBooksCountUseCase
    {
        public async Task<ErrorOr<long>> ExecuteAsync(GetBooksCountRequest request, CancellationToken cancellationToken = default)
        {
            long booksCount = await booksQueryService.CountAsync(cancellationToken);

            if (booksCount < 0)
            {
                return Error.Failure(description: "Failed to retrieve the books count.");
            }

            return booksCount;
        }
    }
}
