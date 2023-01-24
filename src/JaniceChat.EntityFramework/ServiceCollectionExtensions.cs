using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaniceChat.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<ChatContext>()
                .UseInMemoryDatabase(nameof(ChatContext))
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return services;
        }

        private static DatabaseConfiguration GetDatabaseConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfiguration = new DatabaseConfiguration();
            configuration.Bind(ConfigurationKey, databaseConfiguration);
            return databaseConfiguration;
        }

        private static IServiceCollection UseSqlServer(this IServiceCollection services) =>
            services.AddSingleton<IDatabaseProviderResolver, SqlServerDatabaseResolver>();

        private static IServiceCollection UseInMemoryDatabase(this IServiceCollection services) =>
            services.AddSingleton<IDatabaseProviderResolver, InMemoryDatabaseResolver>();
    }
}
