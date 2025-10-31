using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Persistence.Common
{
    public interface IWriteableRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : struct
    {
        Task AddAsync(TEntity entity, CancellationToken ct = default);
        Task AddOrUpdateAsync(TEntity entity, CancellationToken ct = default);
        Task UpdateAsync(TEntity entity, CancellationToken ct = default);
        Task DeleteAsync(TKey id, CancellationToken ct = default);
        Task DeleteAsync(TEntity entity, CancellationToken ct = default);
    }
}
