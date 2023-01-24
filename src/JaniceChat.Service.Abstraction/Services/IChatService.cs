using JaniceChat.Domain;
using System.Threading.Tasks;
using System;

namespace JaniceChat.Service.Abstraction.Services
{
    public interface IChatService
    {
        Task<ChatMessage> CreateMessage(Guid roomId, string userName, string message);
    }
}
