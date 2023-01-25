namespace JaniceChat.Service.Services
{
    public class UserServiceException : Exception
    {
        public UserServiceException(string message, ReasonType reason)
            :base(message)
        {
            Reason = reason;
        }

        public ReasonType Reason { get; }

        public enum ReasonType
        {
            UserAlreadyExists
        }
    }
}
