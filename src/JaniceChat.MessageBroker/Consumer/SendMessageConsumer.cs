using JaniceChat.MessageBroker.Abstraction.Commands;
using JaniceChat.Service.Abstraction.Services;
using MassTransit;

namespace JaniceChat.MessageBroker.Handlers
{
    public class SendMessageConsumer : IConsumer<SendMessageCommand>
    {
        private readonly IChatService _service;

        public SendMessageConsumer(IChatService service)
        {
            _service = service;
        }

        public Task Consume(ConsumeContext<SendMessageCommand> context) =>
            _service.CreateMessage(context.Message.RoomId, context.Message.UserName, context.Message.Message);
    }
}