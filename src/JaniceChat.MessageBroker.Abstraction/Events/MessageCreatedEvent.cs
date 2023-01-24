using System;

namespace JaniceChat.MessageBroker.Abstraction.Events
{
    public class MessageCreatedEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid ChatRoomId { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid UserId { get; set; }
        public string UserUserName { get; set; }
    }
}
