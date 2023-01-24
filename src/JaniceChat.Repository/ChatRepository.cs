using JaniceChat.Domain;
using JaniceChat.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace JaniceChat.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatContext _context;

        public ChatRepository(ChatContext context)
        {
            _context = context;
        }

        public Task<ChatRoom> GetChatRoomById(Guid id) =>
            _context
                .ChatRooms
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id == id);

        public async Task SaveChatRoom(ChatRoom room)
        {
            _context.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChatMessage(ChatMessage chat)
        {
            _context.Add(chat);
            await _context.SaveChangesAsync();
        }

        public Task<List<ChatMessage>> GetMessages(Guid roomId, int skip, int take) =>
            _context
                .ChatMessages
                .AsNoTracking()
                .Include(x=> x.User)
                .Where(x => x.ChatRoomId == roomId)
                .OrderByDescending(x=> x.Date)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

        public Task<List<ChatRoom>> GetAllChatRooms() =>
            _context
                .ChatRooms
                .AsNoTracking()
                .ToListAsync();

        public Task<ChatMessage> GetChatMessageById(Guid id) =>
            _context
                .ChatMessages
                .AsNoTracking()
                .Include(x=> x.User)
                .FirstOrDefaultAsync(x=> x.Id == id);
    }
}
