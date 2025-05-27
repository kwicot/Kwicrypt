using Kwicrypt.Module.Core;
using Kwicrypt.Module.Telegram.Constants;
using Kwicrypt.Module.Telegram.Factorys;
using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;
using Kwicrypt.Module.Telegram.Persistent;
using Kwicrypt.Module.Telegram.Repositorys;
using Kwicrypt.Module.Telegram.Services;
using Kwicrypt.Module.Telegram.Services.Background;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Telegram;

public class TelegramModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));

        services.AddScoped<ITelegramLinkTokenRepository, TelegramLinkTokenRepository>();
        services.AddScoped<ITelegramLinkTokenFactory, TelegramLinkTokenFactory>();

        services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
        services.AddScoped<ITelegramUserFactory, TelegramUserFactory>();

        services.AddScoped<ITelegramService, TelegramService>();
        services.AddScoped<ITelegramLinkService, TelegramLinkService>();

        services.AddHostedService<ExpiredTelegramTokenCleanupService>();

        services.AddHostedService<TelegramBotBackgroundService>();
    }

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.TelegramSettings.json", optional: false, reloadOnChange: false);
        

        //DB contexts
        builder.Services.AddDbContext<TelegramUserDbContext>(options =>
        {
            var optionsFromMethod = TelegramUserDbContext.CreateOptions(Paths.DB_CONTEXT_USERS);
            options.UseSqlite(optionsFromMethod.Extensions.OfType<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>().First().ConnectionString);
        });
        
        builder.Services.AddDbContext<TelegramLinkTokenDbContext>(options =>
        {
            var optionsFromMethod = TelegramLinkTokenDbContext.CreateOptions(Paths.DB_CONTEXT_LINKS);
            options.UseSqlite(optionsFromMethod.Extensions.OfType<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>().First().ConnectionString);
        });
        
        builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));
    }
}