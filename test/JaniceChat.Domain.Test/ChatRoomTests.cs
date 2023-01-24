namespace JaniceChat.Domain.Test
{
    public class ChatRoomTests
    {
        [Fact]
        public void ChatRoom_ShouldHaveUniqueId()
        {
            // Arrange
            var room1 = new ChatRoom();
            var room2 = new ChatRoom();

            // Assert
            Assert.NotEqual(room1.Id, room2.Id);
        }

        [Fact]
        public void ChatRoom_ShouldAddMessage()
        {
            // Arrange
            var room = new ChatRoom();
            var user = new User();
            var message = "Oh my gawd!";

            // Act
            var chat = room.AddMessage(user, message);

            // Assert
            Assert.Equal(chat.ChatRoomId, room.Id);
            Assert.Equal(chat.UserId, user.Id);
            Assert.Equal(chat.Message, message);
            Assert.Contains(chat, room.Chats);
        }
    }
}