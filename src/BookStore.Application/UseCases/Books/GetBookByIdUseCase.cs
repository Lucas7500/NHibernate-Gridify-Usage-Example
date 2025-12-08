using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.QueryServices.Contracts;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.ValueObjects;
using ErrorOr;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class GetBookByIdUseCase(IBooksQueryService booksQueryService) : IGetBookByIdUseCase
    {
        public async Task<ErrorOr<BookWithAuthorResponse>> ExecuteAsync(BookId request, CancellationToken cancellationToken = default)
        {
            BookWithAuthorResponse? book = await booksQueryService.GetByIdAsync(request, cancellationToken);

            if (book is null)
            {
                return Error.NotFound(description: $"Book with Id '{request}' was not found.");
            }

            return book;
        }
    }
}
