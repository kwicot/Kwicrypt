namespace Kwicrypt.Module.Auth.Constants;

public static class Paths
{
    public static string DB_CONTEXT_REFRESH_TOKENS => Path.Combine("..", "Data", "Auth", "refreshTokens.db");
    public static string DB_CONTEXT_USERS => Path.Combine("..", "Data", "Auth", "users.db");
}