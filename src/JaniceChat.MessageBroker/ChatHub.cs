using JaniceChat.MessageBroker.Abstraction.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace JaniceChat.MessageBroker
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(MessageCreatedEvent @event)
            => await Clients.Group($"Room-{@event.ChatRoomId}-Messages").SendAsync("MessageCreatedEvent", @event);

        public async Task JoinChatRoom(string roomId) =>
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Room-{roomId}-Messages");
    }
}
