using JaniceChat.Domain;
using Microsoft.EntityFrameworkCore;

namespace JaniceChat.Repository
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
        }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
    }

}