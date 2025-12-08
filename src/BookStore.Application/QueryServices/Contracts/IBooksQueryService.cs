using BookStore.Application.DTOs.Books.Responses;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects;

namespace BookStore.Application.QueryServices.Contracts
{
    public interface IBooksQueryService
    {
        Task<PagedResult<BookOnlyResponse>> GetAllAsync(QueryRequest request, CancellationToken cancellationToken = default);
        Task<PagedResult<BookWithAuthorResponse>> GetAllWithAuthorsFetchedAsync(QueryRequest request, CancellationToken cancellationToken = default);
        Task<BookWithAuthorResponse?> GetByIdAsync(BookId id, CancellationToken ct = default);
        Task<long> CountAsync(CancellationToken ct = default);
    }
}
