using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;
using Microsoft.Extensions.Options;

namespace Kwicrypt.Module.Telegram.Services.Background;

public class ExpiredTelegramTokenCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpiredTelegramTokenCleanupService> _logger;
    private readonly TelegramSettings _telegramSettings;

    public ExpiredTelegramTokenCleanupService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExpiredTelegramTokenCleanupService> logger,
        IOptions<TelegramSettings> telegramSettings)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _telegramSettings = telegramSettings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var settings = scope.ServiceProvider.GetRequiredService<IOptions<TelegramSettings>>().Value;
            var repo = scope.ServiceProvider.GetRequiredService<ITelegramLinkTokenRepository>();

            var expiredTokens = await repo.GetExpiredTokensAsync(settings.LinkTokenLifetimeMinutes);
            foreach (var token in expiredTokens)
            {
                await repo.DeleteToken(token);
            }

            _logger.LogInformation($"[TelegramTokenCleanup] Removed {expiredTokens.Count} expired tokens");

            await Task.Delay(_telegramSettings.LinkTokenLifetimeMinutes * 60000, stoppingToken);
        }
    }
}
