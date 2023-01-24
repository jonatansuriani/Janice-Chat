namespace JaniceChat.FinBot
{
    public class StockCommand : ICommand
    {
        public StockCommand()
        {

        }

        public StockCommand(string parameter, Guid chatRoomId)
        {
            Parameter = parameter; 
            ChatRoomId = chatRoomId;
        }

        public string Parameter { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}
