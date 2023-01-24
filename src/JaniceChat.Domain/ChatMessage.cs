using System;

namespace JaniceChat.Domain
{
    public class ChatMessage
    {
        public ChatMessage()
        {

        }

        public ChatMessage(ChatRoom room, User user, string message)
        {
            Id = Guid.NewGuid();
            Message = message;
            UserId = user.Id;
            ChatRoomId = room.Id;
            Date = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }

        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public DateTimeOffset Date { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}