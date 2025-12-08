using BookStore.Application.QueryServices.Contracts;
using BookStore.Domain.Persistence.Contracts.Authors;
using BookStore.Domain.Persistence.Contracts.Books;
using BookStore.Infra.Migrations.Migrations;
using BookStore.Infra.NHibernate;
using BookStore.Infra.QueryServices.Authors;
using BookStore.Infra.QueryServices.Books;
using BookStore.Infra.Repositories.Authors;
using BookStore.Infra.Repositories.Books;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace BookStore.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDatabaseMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").Value;
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
            }

            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(_20251101173000_CreateBookAndAuthorTables).Assembly).For.Migrations()
                );
        }

        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'PostgresConnectionString' not found.");
            }

            var sessionFactory = Fluently
                .Configure()
                .Database(PostgreSQLConfiguration.Standard
                    .Dialect<PostgreSQL82Dialect>()
                    .Driver<NpgsqlDriver>()
                    .ConnectionString(connectionString)
                    .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(DependencyInjectionExtensions).Assembly))
                .BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped<NHibernateContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBooksQueryService, BooksLinqQueryService>();
            services.AddScoped<IAuthorsQueryService, AuthorsLinqQueryService>();

            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        }

        public static void RunDatabaseMigrations(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
