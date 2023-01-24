using Microsoft.Extensions.DependencyInjection;

namespace JaniceChat.MessageBroker
{
    public static class SignalRServiceCollectionExtensions
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection services) 
        {
            services.AddSignalR();
            services.AddScoped<ChatHub>();
            return services;
        }
    }
}
