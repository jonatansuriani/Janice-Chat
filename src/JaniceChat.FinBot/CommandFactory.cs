using System.Collections;

namespace JaniceChat.FinBot
{
    public class CommandFactory 
    {
        public static ICommand Create(string message, Guid chatRoomId)
        {
            if (!message.StartsWith("/")) return null;
            var arr = message.Split(" ");
            if (arr.Length != 2) return null;

            var commandName = GetCommandName(arr);
            return CreateCommand[commandName](GetParameter(arr), chatRoomId);
        }

        private static string GetCommandName(string[] arr) => arr.First().Substring(1);
        private static string GetParameter(string[] arr) => arr.Last();

        private static IDictionary<string, Func<string, Guid, ICommand>> CreateCommand = new Dictionary<string, Func<string, Guid, ICommand>>()
        { 
            ["stock"] = (param, room) => new StockCommand(param, room) 
        };
    }
}