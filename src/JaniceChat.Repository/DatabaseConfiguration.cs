namespace JaniceChat.Repository
{
    public class DatabaseConfiguration
    {
        public DatabaseProviders Provider { get; set; }
        public string ConnectionString { get; set; }
    }

    public enum DatabaseProviders
    {
        InMemory,
        SqlServer
    }
}
