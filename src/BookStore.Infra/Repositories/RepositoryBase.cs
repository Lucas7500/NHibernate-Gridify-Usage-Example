using BookStore.Domain.Models.Base;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Domain.ValueObjects.Contracts;
using NHibernate;
using NHibernate.Linq;

namespace BookStore.Infra.Repositories
{
    internal abstract class RepositoryBase<TEntity, TKey, TKeyValue>(ISession session) : IRepository<TEntity, TKey, TKeyValue>
        where TKeyValue : struct
        where TKey : IStronglyTypedId<TKeyValue>
        where TEntity : AggregateRoot<TKey, TKeyValue>
    {
        protected readonly ISession Session = session;

        public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
            return await Session.GetAsync<TEntity>(id, ct);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await Session.SaveAsync(entity, ct);
        }

        public virtual async Task AddOrUpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            await Session.SaveOrUpdateAsync(entity, ct);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            await Session.UpdateAsync(entity, ct);
        }

        public virtual async Task DeleteAsync(TKey id, CancellationToken ct = default)
        {
            await Session
                .Query<TEntity>()
                .Where(e => e.IdValue.Equals(id.Value))
                .DeleteAsync(ct);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            await Session.DeleteAsync(entity, ct);
        }
    }
}
