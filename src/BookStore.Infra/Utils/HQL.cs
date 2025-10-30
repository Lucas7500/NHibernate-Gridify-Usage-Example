using BookStore.Domain.Models.Base;
using NHibernate;

namespace BookStore.Infra.Utils
{
    internal static class HQL
    {
        public static IQuery GetAllQuery<TEntity>(ISession session) where TEntity : Entity
        {
            return session
                .CreateQuery("from :table")
                .SetParameter("table", typeof(TEntity).Name);
        }
        
        public static IQuery GetByIdQuery<TEntity, TKey>(ISession session, TKey id) 
            where TEntity : Entity<TKey>
            where TKey : struct
        {
            return session
                .CreateQuery("from entity :table e where e.Id = :id")
                .SetParameter("table", typeof(TEntity).Name)
                .SetParameter("id", id);
        }

        public static IQuery CountQuery<TEntity>(ISession session) where TEntity : Entity
        {
            return session
                .CreateQuery("select count(*) from :table")
                .SetParameter("table", typeof(TEntity).Name);
        }
    }
}
