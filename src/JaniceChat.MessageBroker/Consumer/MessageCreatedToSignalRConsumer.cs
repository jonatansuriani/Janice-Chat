using JaniceChat.MessageBroker.Abstraction.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace JaniceChat.MessageBroker.Handlers
{
    public class MessageCreatedToSignalRConsumer : IConsumer<MessageCreatedEvent>
    {
        private readonly IHubContext<ChatHub> _hub;
        public MessageCreatedToSignalRConsumer(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public Task Consume(ConsumeContext<MessageCreatedEvent> context) =>
            _hub.Clients
                .Group($"Room-{context.Message.ChatRoomId}-Messages")
                .SendAsync("MessageCreatedEvent", context.Message);
    }
}