using Kwicrypt.Module.Core;
using Kwicrypt.Module.Telegram.Constants;
using Kwicrypt.Module.Telegram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kwicrypt.Module.Telegram.Persistent;

public class TelegramLinkTokenDbContextFactory : IDesignTimeDbContextFactory<TelegramLinkTokenDbContext>
{
    public TelegramLinkTokenDbContext CreateDbContext(string[] args) =>
        new (TelegramLinkTokenDbContext.CreateOptions(Constants.Paths.DB_CONTEXT_LINKS));
}

public class TelegramLinkTokenDbContext : DBContextBase<TelegramLinkTokenDbContext,TelegramLinkToken>
{
    public TelegramLinkTokenDbContext(DbContextOptions<TelegramLinkTokenDbContext> options) : base(options)
    {
    }
}