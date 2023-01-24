using AutoMapper;
using JaniceChat.Domain;
using JaniceChat.MessageBroker.Abstraction.Events;
using JaniceChat.Repository.Abstraction;
using JaniceChat.Service.Abstraction.Services;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace JaniceChat.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository, IBus bus, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public async Task<ChatMessage> CreateMessage(Guid roomId, string userName, string message)
        {
            var room = await _chatRepository.GetChatRoomById(roomId);
            var user = await _userRepository.GetUserByUserName(userName);

            var chat = room.AddMessage(user, message);

            await _chatRepository.SaveChatMessage(chat);

            await _bus.Publish(_mapper.Map<MessageCreatedEvent>(
                await _chatRepository.GetChatMessageById(chat.Id)));

            return chat;
        }
    }
}