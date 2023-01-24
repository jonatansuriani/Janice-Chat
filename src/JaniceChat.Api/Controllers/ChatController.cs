using AutoMapper;
using JaniceChat.Api.Models;
using JaniceChat.Domain;
using JaniceChat.MessageBroker.Abstraction.Commands;
using JaniceChat.Repository.Abstraction;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JaniceChat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public ChatController(IBus bus, IChatRepository chatRepository, IMapper mapper)
        {
            _bus = bus;
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        [HttpPost("room/{roomId}/message")]
        public async Task<IActionResult> SendMessage(Guid roomId, [FromBody] SendMessageRequestModel model) 
        {
            await _bus.Publish(new SendMessageCommand { Message = model.Message, RoomId = roomId, UserName = User.Identity.Name });
            
            return Accepted();
        }

        [HttpGet("room/{roomId}/messages")]
        public async Task<ActionResult<List<ChatMessageModel>>> GetMessages(Guid roomId, int skip, int take)
        {
            var messsages = await _chatRepository.GetMessages(roomId, skip, take);
            return Ok(_mapper.Map<List<ChatMessageModel>>(messsages));
        }

        [HttpPost("room")]
        public async Task<ActionResult<ChatRoomModel>> CreateChatRoom([FromBody] CreateChatRoomRequestModel model)
        {
            var room = new ChatRoom() { Name = model.Name };

            await _chatRepository.SaveChatRoom(room);

            return Created($"/chat/rooms/{room.Id}", _mapper.Map<ChatRoomModel>(room));
        }

        [HttpGet("rooms")]
        public async Task<ActionResult<List<ChatRoomModel>>> GetChatRooms()
        {
            var rooms = await _chatRepository.GetAllChatRooms();

            return Ok(_mapper.Map<List<ChatRoomModel>>(rooms));
        }
    }
}
