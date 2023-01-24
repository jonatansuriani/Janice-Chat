using Microsoft.Extensions.Configuration;

namespace JaniceChat.MessageBroker.Abstraction
{
    public static class MessageBrokerConfigurationExtensions
    {
        public static MessageBrokerConfiguration GetMessageBrokerConfiguration(this IConfiguration configuration) 
        {
            var brokerConfiguration = new MessageBrokerConfiguration();
            configuration.Bind("MessageBroker", brokerConfiguration);
            return brokerConfiguration;
        }
    }
}
