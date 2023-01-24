using JaniceChat.Domain;
using System;
using System.Threading.Tasks;

namespace JaniceChat.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUserName(string email);
        Task<User> Create(string email);
    }
}
