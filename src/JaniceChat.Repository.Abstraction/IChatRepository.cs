using JaniceChat.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaniceChat.Repository.Abstraction
{
    public interface IChatRepository
    {
        Task<ChatRoom> GetChatRoomById(Guid id);
        Task<ChatMessage> GetChatMessageById(Guid id);
        Task<List<ChatRoom>> GetAllChatRooms();
        Task SaveChatMessage(ChatMessage chat);
        Task SaveChatRoom(ChatRoom room);

        Task<List<ChatMessage>> GetMessages(Guid roomId, int skip, int take);
    }
}