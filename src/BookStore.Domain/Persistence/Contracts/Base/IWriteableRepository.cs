using BookStore.Domain.Models.Base;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Domain.Persistence.Contracts.Base
{
    public interface IWriteableRepository<TEntity, TKey, TKeyValue>
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
        where TEntity : AggregateRoot<TKey, TKeyValue>
    {
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task AddOrUpdateAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(TKey id, CancellationToken ct = default);
        Task DeleteAsync(TEntity entity, CancellationToken ct = default);
    }
}
