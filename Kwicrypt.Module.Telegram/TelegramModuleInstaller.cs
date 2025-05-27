using Backend.Modules.Telegram.Factorys;
using Backend.Modules.Telegram.Interfaces;
using Backend.Modules.Telegram.Models;
using Backend.Modules.Telegram.Repositorys;
using Backend.Modules.Telegram.Services;

namespace Backend.Modules.Telegram;

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
        
        builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));
    }
}