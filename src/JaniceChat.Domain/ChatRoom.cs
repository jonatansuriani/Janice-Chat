using System;
using System.Collections.Generic;

namespace JaniceChat.Domain
{
    public class ChatRoom
    {
        public ChatRoom()
        {
            Id = Guid.NewGuid();
            Chats = new List<ChatMessage>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public IList<ChatMessage> Chats { get; }

        public ChatMessage AddMessage(User user, string message)
        {
            var chat = new ChatMessage(this, user, message);
            Chats.Add(chat);
            return chat;
        }
    }
}
