using JaniceChat.MessageBroker.Abstraction.Events;
using MassTransit;

namespace JaniceChat.FinBot.Consumers
{
    public class MessageCreatedToSendCommandConsumer : IConsumer<MessageCreatedEvent>
    {
        private readonly IBus _bus;

        public MessageCreatedToSendCommandConsumer(IBus bus)
        {
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<MessageCreatedEvent> context)
        {
            var message = context.Message.Message;

            var command = CommandFactory.Create(message, context.Message.ChatRoomId);
            if (command == null) return;

            await _bus.Publish(command, command.GetType());
        }
    }
}