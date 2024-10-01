namespace NoteApp.Domain;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static DatabaseConfiguration Database { get; set; } = new();
    public static EmailConfiguration Email { get; set; } = new();
    public static SendGridConfiguration SendGrid { get; set; } = new();
    public static PaginationConfiguration Pagination { get; set; } = new();

    public class PaginationConfiguration
    {
        public int DefaultPageNumber { get; set; } 
        public int DefaultPageSize { get; set; }
    }
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
    public class EmailConfiguration
    {
        public string DefaultFromEmail { get; set; } = string.Empty;
        public string DefaultFromName { get; set; } = string.Empty;
    }
    public class SecretsConfiguration
    {
        public string JwtPrivateKey { get; set; } = string.Empty;
    }
    public class SendGridConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
