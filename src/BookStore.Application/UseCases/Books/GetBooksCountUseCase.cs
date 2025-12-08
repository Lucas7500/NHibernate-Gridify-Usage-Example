using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Persistence.Contracts.Books;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class GetBooksCountUseCase(IBooksQueryService booksQueryService) : IGetBooksCountUseCase
    {
        public async Task<long> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return await booksQueryService.CountAsync(cancellationToken);
        }
    }
}
