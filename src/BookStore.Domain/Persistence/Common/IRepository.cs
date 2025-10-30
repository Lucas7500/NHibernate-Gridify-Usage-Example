using BookStore.Domain.Models.Base;

namespace BookStore.Domain.Persistence.Common
{
    public interface IRepository<TEntity, TKey> : IQueryableRepository<TEntity, TKey>, IWriteableRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey> 
        where TKey : struct;
}
