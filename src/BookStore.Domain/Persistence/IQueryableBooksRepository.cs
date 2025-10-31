using BookStore.Domain.Models;
using BookStore.Domain.Persistence.Common;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Domain.Persistence
{
    public interface IQueryableBooksRepository : IQueryableRepository<Book, int>
    {
        Task<PagedResult<Book>> GetBooksWithAuthorsFetchedAsync(QueryRequest request, CancellationToken ct = default);
        Task<PagedResult<Author>> GetAllAuthorsAsync(QueryRequest request, CancellationToken ct = default); 
        Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken ct = default); 
        
        Task<long> CountAuthorsAsync(CancellationToken ct = default);
    }
}
