using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Persistence.Common
{
    public interface IWriteableRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : struct
    {
        Task Add(TEntity entity, CancellationToken ct = default);
        Task AddOrUpdate(TEntity entity, CancellationToken ct = default);
        Task Update(TEntity entity, CancellationToken ct = default);
        Task Delete(TKey id, CancellationToken ct = default);
        Task Delete(TEntity entity, CancellationToken ct = default);
    }
}
