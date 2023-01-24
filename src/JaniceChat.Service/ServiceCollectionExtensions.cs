using JaniceChat.Service.Abstraction.Services;
using JaniceChat.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JaniceChat.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddScoped<IChatService, ChatService>();
    }
}
