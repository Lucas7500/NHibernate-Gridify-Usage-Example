using BookStore.Domain.Models;
using BookStore.Domain.Models.Base;
using NHibernate;

namespace BookStore.Infra.Utils
{
    internal static class SQL
    {
        public static ISQLQuery SelectAllQuery<TEntity>(ISession session) where TEntity : Entity
        {
            string tableName = GetTableNameFor<TEntity>();
            string sql = $"SELECT * FROM {tableName}";

            return session.CreateSQLQuery(sql);
        }

        public static ISQLQuery GetByIdQuery<TEntity, TKey>(ISession session, TKey id) 
            where TEntity : Entity<TKey>
            where TKey : struct
        {
            string tableName = GetTableNameFor<TEntity>();
            string sql = $"SELECT * FROM {tableName} WHERE Id = :id";

            ISQLQuery query = session.CreateSQLQuery(sql);
            query.SetParameter("id", id);
         
            return query;
        }

        public static ISQLQuery DeleteByIdQuery<TEntity, TKey>(ISession session, TKey id) 
            where TEntity : Entity<TKey>
            where TKey : struct
        {
            string tableName = GetTableNameFor<TEntity>();
            string sql = $"DELETE FROM {tableName} WHERE Id = :id";
            
            ISQLQuery query = session.CreateSQLQuery(sql);
            query.SetParameter("id", id);
         
            return query;
        }

        public static ISQLQuery DeleteEntityQuery<TEntity>(ISession session, TEntity entity) where TEntity : Entity
        {
            string tableName = GetTableNameFor<TEntity>();
            string sql = $"DELETE FROM {tableName} WHERE Id = :id";
            
            ISQLQuery query = session.CreateSQLQuery(sql);
            query.SetParameter("id", entity.Id);
         
            return query;
        }

        public static ISQLQuery CountQuery<TEntity>(ISession session) where TEntity : Entity
        {
            string tableName = GetTableNameFor<TEntity>();
            string sql = $"SELECT COUNT(*) FROM {tableName}";

            return session.CreateSQLQuery(sql);
        }

        private static string GetTableNameFor<TEntity>() where TEntity : Entity
        {
            const string BooksTableName = "books";
            const string AuthorsTableName = "authors";

            return typeof(TEntity).Name switch
            {
                nameof(Book) => BooksTableName,
                nameof(Author) => AuthorsTableName,
                _ => throw new NotSupportedException($"GetTableName is not supported for {typeof(TEntity).Name}")
            };
        }
    }
}
