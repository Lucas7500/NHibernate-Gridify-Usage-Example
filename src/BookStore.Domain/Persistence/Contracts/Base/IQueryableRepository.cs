using BookStore.Domain.Models.Base;
using BookStore.Domain.Persistence.Requests;
using BookStore.Domain.Persistence.Responses;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Persistence.Contracts.Base
{
    public interface IQueryableRepository<TEntity, TKey, TKeyValue>
        where TKey : IStronglyTypedId<TKeyValue>
        where TKeyValue : struct
        where TEntity : AggregateRoot<TKey, TKeyValue>
    {
        Task<PagedResult<TEntity>> GetAllAsync(QueryRequest request, CancellationToken ct = default);
        Task<TEntity?> GetAsync(TKey id, CancellationToken ct = default);
        Task<long> CountAsync(CancellationToken ct = default);
    }
}
