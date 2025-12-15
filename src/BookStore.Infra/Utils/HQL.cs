using BookStore.Domain.Models.Base;
using BookStore.Domain.ValueObjects.Contracts;
using NHibernate;

namespace BookStore.Infra.Utils
{
    internal static class HQL
    {
        internal static IQuery GetAllQuery<TEntity>(ISession session) where TEntity : Entity
        {
            return session
                .CreateQuery("from :table")
                .SetParameter("table", typeof(TEntity).Name);
        }

        internal static IQuery GetByIdQuery<TEntity, TKey, TKeyValue>(ISession session, TKey id)
            where TKeyValue : struct
            where TKey : IStronglyTypedId<TKeyValue>
            where TEntity : Entity<TKey, TKeyValue>
        {
            return session
                .CreateQuery("from entity :table e where e.Id = :id")
                .SetParameter("table", typeof(TEntity).Name)
                .SetParameter("id", id.Value);
        }

        internal static IQuery CountQuery<TEntity>(ISession session) where TEntity : Entity
        {
            return session
                .CreateQuery("select count(*) from :table")
                .SetParameter("table", typeof(TEntity).Name);
        }
    }
}
