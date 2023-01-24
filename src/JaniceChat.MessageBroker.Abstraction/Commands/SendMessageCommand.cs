using System;

namespace JaniceChat.MessageBroker.Abstraction.Commands
{
    public class SendMessageCommand
    {
        public string UserName { get; set; }
        public Guid RoomId { get; set; }
        public string Message { get; set; }
    }
}