using JaniceChat.Domain;
using JaniceChat.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace JaniceChat.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext _context;

        public UserRepository(ChatContext context)
        {
            _context = context;
        }

        public async Task<User> Create(string userName)
        {
            var user = new User() { UserName = userName };
            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task<User> GetUserById(Guid id) =>
            _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public Task<User> GetUserByUserName(string userName) =>
            _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == userName);
    }
}
