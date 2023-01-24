using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaniceChat.Domain.Test
{
    public class ChatMessageTests
    {
        [Fact]
        public void ChatMessage_ShouldHaveUniqueId()
        {
            // Arrange
            // Arrange
            var room = new ChatRoom();
            var user = new User();
            var message = "Lorem Ipsum";

            var message1 = new ChatMessage(room, user, message);
            var message2 = new ChatMessage(room, user, message);

            // Assert
            Assert.NotEqual(message1.Id, message2.Id);
        }

        [Fact]
        public void ChatMessage_ShouldHaveCorrectProperties()
        {
            // Arrange
            var room = new ChatRoom();
            var user = new User();
            var message = "Lorem Ipsum";

            // Act
            var chat = new ChatMessage(room, user, message);

            // Assert
            Assert.Equal(chat.ChatRoomId, room.Id);
            Assert.Equal(chat.UserId, user.Id);
            Assert.Equal(chat.Message, message);
        }
    }
}
