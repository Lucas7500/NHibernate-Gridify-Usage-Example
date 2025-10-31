using BookStore.Domain.Models.Base;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;

namespace BookStore.Domain.Persistence.Common
{
    public interface IQueryableRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : struct
    {
        Task<PagedResult<TEntity>> GetAllAsync(QueryRequest request, CancellationToken ct = default);
        Task<TEntity?> GetAsync(TKey id, CancellationToken ct = default);

        Task<long> CountAsync(CancellationToken ct = default);
    }
}
