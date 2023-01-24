namespace JaniceChat.Service.Services
{
    public class ChatServiceException : Exception
    {
        public ChatServiceException() 
        { }

        public ReasonType Reason { get; set; }

        public enum ReasonType
        {
            UserNotFound,
            RoomNotFound
        }
    }
}
