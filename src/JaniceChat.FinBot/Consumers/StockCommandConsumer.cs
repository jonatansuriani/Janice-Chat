using JaniceChat.MessageBroker.Abstraction.Commands;
using MassTransit;

namespace JaniceChat.FinBot.Consumers
{
    public class StockCommandConsumer : IConsumer<StockCommand>
    {
        private readonly IBus _bus;
        private readonly IStockGateway _gateway;

        public StockCommandConsumer(IBus bus, IStockGateway gateway)
        {
            _bus = bus;
            _gateway = gateway;
        }

        public async Task Consume(ConsumeContext<StockCommand> context)
        {
            var stockInfo = await _gateway.GetStock(context.Message.Parameter);
            if (!stockInfo.Any()) return;

            var stock = stockInfo.First();

            var message = $"{stock.Symbol} quote is {stock.Close} per share";

            await _bus.Publish(new SendMessageCommand { Message = message, RoomId = context.Message.ChatRoomId, UserName = "FinBot" });
        }
    }
}