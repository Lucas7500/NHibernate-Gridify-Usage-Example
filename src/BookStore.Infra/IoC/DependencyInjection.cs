using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Infra.Repositories;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace BookStore.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBooksRepository, BooksRepository>();

            const string DbFileName = "BookStore.db";

            var sessionFactory = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard
                    .Dialect<SQLiteDialect>()
                    .Driver<SQLite20Driver>()
                    .UsingFile(DbFileName)
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(DependencyInjection).Assembly))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();
            
            services.AddScoped(_ =>
            {
                return sessionFactory.OpenSession();
            });
        }
    }
}
