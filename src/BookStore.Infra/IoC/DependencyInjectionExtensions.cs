using BookStore.Domain.Models;
using BookStore.Domain.Persistence;
using BookStore.Infra.NHibernate;
using BookStore.Infra.Repositories;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace BookStore.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IQueryableBooksRepository, BooksRepositoryHQL>();

            const string DbFileName = "BookStore.db";

            var sessionFactory = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard
                    .Dialect<SQLiteDialect>()
                    .Driver<SQLite20Driver>()
                    .UsingFile(DbFileName)
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(DependencyInjectionExtensions).Assembly))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();
            
            services.AddSingleton(sessionFactory);
            services.AddScoped<NHibernateContext>();
        }
    }
}
