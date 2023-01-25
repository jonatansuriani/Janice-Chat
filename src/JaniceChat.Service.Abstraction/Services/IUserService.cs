using JaniceChat.Domain;
using System.Threading.Tasks;

namespace JaniceChat.Service.Abstraction.Services
{
    public interface IUserService
    {
        Task<User> Create(string userName);
    }
}
