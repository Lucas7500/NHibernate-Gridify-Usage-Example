using BookStore.Application.DTOs.Books.Responses;
using BookStore.Application.Mappers;
using BookStore.Application.UseCases.Books.Contracts;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Application.UseCases.Books
{
    internal sealed class GetBooksUseCase(IQueryableBooksRepository booksRepository) : IGetBooksUseCase
    {
        public async Task<PagedResult<BookResponse>> ExecuteAsync(QueryRequest request, CancellationToken cancellationToken = default)
        {
            PagedResult<Book> pagedBooks = await booksRepository.GetAllAsync(request, cancellationToken);

            IEnumerable<BookResponse> booksResponse = pagedBooks
                .Select(book => book.ToResponse())
                .ToList();

            return new PagedResult<BookResponse>(
                pagedBooks.CurrentPage,
                pagedBooks.PageSize,
                pagedBooks.TotalCount,
                booksResponse);
        }
    }
}
