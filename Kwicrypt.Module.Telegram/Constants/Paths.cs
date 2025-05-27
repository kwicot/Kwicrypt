namespace Kwicrypt.Module.Telegram.Constants;

public static class Paths
{
    public static string DB_CONTEXT_LINKS => Path.Combine("..", "Data", "Telegram", "connectionLinks.db");
    public static string DB_CONTEXT_USERS => Path.Combine("..", "Data", "Telegram", "users.db");
}