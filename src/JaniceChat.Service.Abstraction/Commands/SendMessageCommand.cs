using System;

namespace JaniceChat.Service.Abstraction.Commands
{
    public class SendMessageCommand
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public string Message { get; set; }
    }
}
