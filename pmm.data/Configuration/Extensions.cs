using System;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using pmm.data.Context;
using pmm.data.Context.Interfaces;

namespace pmm.data.Configuration
{
	public static class Extensions
	{
        public static IServiceCollection AddHibernateWebContext(this IServiceCollection services,
            string connectionString)
        {
            var factory =
                Fluently
                    .Configure()
                    .Database(
                        PostgreSQLConfiguration.PostgreSQL83.ConnectionString(connectionString)
                            .Dialect<PostgreSQL83Dialect>())
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<IDbContext>())
                    .CurrentSessionContext("web")
                    .BuildSessionFactory();

            services.AddSingleton(factory);
            services.AddScoped<IDbContext, DbContext>();

            AddFluentMigrator(services, connectionString);

            return services;
        }

        public static void PerformDatabaseMigrations(this IServiceProvider provider)
		{
            using (var scope = provider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
		}

		private static void AddFluentMigrator(IServiceCollection services, string connectionString)
		{
			services.AddFluentMigratorCore()
				.ConfigureRunner(x =>
					x.AddPostgres()
						.WithGlobalConnectionString(connectionString)
						.WithGlobalCommandTimeout(TimeSpan.FromSeconds(10 * 60))
						.ScanIn(typeof(IDbContext).Assembly)
						.For.All())
				.AddLogging(x => x.AddFluentMigratorConsole())
				.BuildServiceProvider(false);
		}
	}
}