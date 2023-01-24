using AutoMapper;
using JaniceChat.Domain;
using JaniceChat.MessageBroker.Abstraction.Events;

namespace JaniceChat.Service
{
    public static class MapperConfigurationExtensions
    {
        public static IMapperConfigurationExpression AddServiceMapperConfiguration(this IMapperConfigurationExpression config)
        {
            config.CreateMap<ChatMessage, MessageCreatedEvent>();
            return config;
        }
    }
}
