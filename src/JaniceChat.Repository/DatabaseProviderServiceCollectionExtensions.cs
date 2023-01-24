using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JaniceChat.Repository
{
    public static class DatabaseProviderServiceCollectionExtensions
    {
        private const string ConfigurationKey = "Database";

        public static IServiceCollection AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfiguration = GetDatabaseConfiguration(configuration);

            providerAction[databaseConfiguration.Provider](services, databaseConfiguration);

            return services
                .AddDbContext<ChatContext>();
        }

        static Dictionary<DatabaseProviders, Action<IServiceCollection, DatabaseConfiguration>> providerAction = new Dictionary<DatabaseProviders, Action<IServiceCollection, DatabaseConfiguration>>()
        {
            [DatabaseProviders.InMemory] = (services, configuration) => AddInMemoryProvider(services)
        };

        private static IServiceCollection AddInMemoryProvider(IServiceCollection services) =>
            services
                .AddSingleton(_ => new DbContextOptionsBuilder<ChatContext>()
                        .UseInMemoryDatabase(nameof(ChatContext)).Options);


        private static DatabaseConfiguration GetDatabaseConfiguration(IConfiguration configuration)
        {
            var databaseConfiguration = new DatabaseConfiguration();
            configuration.Bind(ConfigurationKey, databaseConfiguration);
            return databaseConfiguration;
        }
    }
}
