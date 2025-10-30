using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Persistence.Common
{
    public interface IQueryableRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken ct = default);
        Task<TEntity?> Get(TKey id, CancellationToken ct = default);

        Task<long> Count(CancellationToken ct = default);
    }
}
