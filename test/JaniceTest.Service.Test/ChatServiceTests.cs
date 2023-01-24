using AutoMapper;
using FluentAssertions;
using JaniceChat.Domain;
using JaniceChat.MessageBroker.Abstraction.Events;
using JaniceChat.Repository.Abstraction;
using JaniceChat.Service.Services;
using MassTransit;
using Moq;

namespace JaniceTest.Service.Test
{
    public class ChatServiceTests
    {
        private readonly Mock<IChatRepository> _chatRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IBus> _busMock;
        private readonly IMapper _mapper;
        private readonly ChatService _chatService;

        static Guid ExistingRoomId = Guid.NewGuid();
        static string ExistingUserName = "Janice";

        User ExistingUser = new User() { UserName = ExistingUserName };
        ChatRoom ExistingRoom = new ChatRoom() { Id = ExistingRoomId };

        public ChatServiceTests()
        {
            _chatRepositoryMock = new Mock<IChatRepository>();
            _chatRepositoryMock.Setup(x => x.GetChatRoomById(ExistingRoomId)).ReturnsAsync(ExistingRoom);

            _userRepositoryMock = new Mock<IUserRepository>();
            _userRepositoryMock.Setup(x => x.GetUserByUserName(ExistingUserName)).ReturnsAsync(ExistingUser);

            _busMock = new Mock<IBus>();

            _mapper = new MapperConfiguration(config => 
                config.CreateMap<ChatMessage, MessageCreatedEvent>()
            ).CreateMapper();

            _chatService = new ChatService(_chatRepositoryMock.Object, _userRepositoryMock.Object, _busMock.Object, _mapper);
        }

        [Fact]
        public async void CreateMessage_ShouldAddMessageToRoom()
        {
            // Arrange
            var message = "Oh my gawd!";
            var chat = new ChatMessage(ExistingRoom, ExistingUser, message);

            _chatRepositoryMock.Setup(x => x.SaveChatMessage(chat)).Returns(Task.CompletedTask);
            _chatRepositoryMock.Setup(x => x.GetChatMessageById(It.IsAny<Guid>())).ReturnsAsync(chat);

            // Act
            var result = await _chatService.CreateMessage(ExistingRoomId, ExistingUserName, message);

            // Assert
            result.Should().BeEquivalentTo(new { chat.ChatRoomId, chat.Message, chat.UserId }, x => x.ExcludingMissingMembers());

            _chatRepositoryMock.Verify(x => x.GetChatRoomById(ExistingRoomId), Times.Once);
            _userRepositoryMock.Verify(x => x.GetUserByUserName(ExistingUserName), Times.Once);
            _chatRepositoryMock.Verify(x => x.SaveChatMessage(It.IsAny<ChatMessage>()), Times.Once); ;
            _busMock.Verify(x => 
                x.Publish(It.Is<MessageCreatedEvent>(y=> 
                    y.Message == message && y.UserId == ExistingUser.Id && y.ChatRoomId == ExistingRoomId 
                ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void CreateMessage_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var userName = "_Janice_";
            var message = "Oh my gawd!";

            // Act & Assert
            var result = await Assert.ThrowsAsync<ChatServiceException>(() => 
                _chatService.CreateMessage(ExistingRoomId, userName, message));
            result.Reason.Should().Be(ChatServiceException.ReasonType.UserNotFound);
        }

        [Fact]
        public async void CreateMessage_ShouldThrowException_WhenRoomNotFound()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var message = "Oh my gawd!";

            // Act & Assert
            var result = await Assert.ThrowsAsync<ChatServiceException>(() =>
                _chatService.CreateMessage(roomId, ExistingUserName, message));
            result.Reason.Should().Be(ChatServiceException.ReasonType.RoomNotFound);
        }
    }
}