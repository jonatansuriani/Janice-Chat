using JaniceChat.Domain;
using JaniceChat.Repository.Abstraction;
using JaniceChat.Service.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaniceChat.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Create(string userName)
        {
            if (await _repository.GetUserByUserName(userName) != null)
                throw new UserServiceException("User Already Exists", UserServiceException.ReasonType.UserAlreadyExists);

            return await _repository.Create(userName);
        }
    }
}
