using Kwicrypt.Module.Core;
using Kwicrypt.Module.Telegram.Constants;
using Kwicrypt.Module.Telegram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kwicrypt.Module.Telegram.Persistent;

public class TelegramUserDbContextFactory : IDesignTimeDbContextFactory<TelegramUserDbContext>
{
    public TelegramUserDbContext CreateDbContext(string[] args) =>
        new (TelegramUserDbContext.CreateOptions(Constants.Paths.DB_CONTEXT_USERS));
}
public class TelegramUserDbContext : DBContextBase<TelegramUserDbContext,TelegramUser>
{
    public TelegramUserDbContext(DbContextOptions<TelegramUserDbContext> options) : base(options)
    {
    }
}