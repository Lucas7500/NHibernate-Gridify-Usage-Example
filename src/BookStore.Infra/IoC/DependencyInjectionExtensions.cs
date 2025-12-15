using BookStore.Domain.Persistence.Contracts;
using BookStore.Infra.NHibernate;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace BookStore.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDatabaseMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetSection("ConnectionString").Value;
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
            }

            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(AssemblyReference).Assembly).For.Migrations()
                );
        }

        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetSection("ConnectionString").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'PostgresConnectionString' not found.");
            }

            ISessionFactory sessionFactory = Fluently
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

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, NHibernateUnitOfWork>();
        }

        public static void RunDatabaseMigrations(this IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            
            IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
