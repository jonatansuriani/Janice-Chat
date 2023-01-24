namespace JaniceChat.FinBot
{
    public interface ICommand
    {
        string Parameter { get; set; }
        Guid ChatRoomId { get; set; }
    }
}
