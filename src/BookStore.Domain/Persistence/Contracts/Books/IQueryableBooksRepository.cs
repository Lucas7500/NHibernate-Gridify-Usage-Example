using BookStore.Domain.Models.BookModel;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;

namespace BookStore.Domain.Persistence.Contracts.Books
{
    public interface IQueryableBooksRepository : IQueryableRepository<Book, BookId, int>
    {
        Task<PagedResult<Book>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default);
    }
}
