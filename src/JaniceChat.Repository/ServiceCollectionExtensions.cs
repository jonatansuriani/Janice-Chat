using JaniceChat.Repository.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JaniceChat.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped<IChatRepository, ChatRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddDatabaseProvider(configuration);
    }
}