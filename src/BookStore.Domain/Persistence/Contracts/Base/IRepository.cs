using BookStore.Domain.Models.Base;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Persistence.Contracts.Base
{
    public interface IRepository<TEntity, TKey, TKeyValue>
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
        where TEntity : AggregateRoot<TKey, TKeyValue>
    {
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task AddOrUpdateAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(TKey id, CancellationToken ct = default);
        Task DeleteAsync(TEntity entity, CancellationToken ct = default);
    }
}
