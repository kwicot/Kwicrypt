using Backend.Modules.Telegram.Interfaces;
using Backend.Modules.Telegram.Models;
using Backend.Modules.Telegram.Services;
using Backend.Modules.Auth.Models;
using Backend.Token;
using Microsoft.Extensions.Options;

namespace Backend.Modules.Auth.Services.Background;

public class ExpiredRefreshTokenCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ExpiredRefreshTokenCleanupBackgroundService> _logger;
    private JwtSettings _jwtSettings;
    private IRefreshTokenRepository _refreshTokenRepository;

    public ExpiredRefreshTokenCleanupBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<ExpiredRefreshTokenCleanupBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            _jwtSettings = scope.ServiceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;
            Console.WriteLine($"Loaded JWT settings: Interval- {_jwtSettings.RefreshTokenCleanupIntervalMinutes}");
            _refreshTokenRepository = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();

            var expiredTokens = await _refreshTokenRepository.GetExpiredTokensAsync(_jwtSettings.RefreshTokenExpirationMinutes);
            foreach (var token in expiredTokens)
            {
                await _refreshTokenRepository.DeleteAsync(token);
            }

            _logger.LogInformation($"[RefreshTokenCleanup] Removed {expiredTokens.Count} expired tokens");

            await Task.Delay(_jwtSettings.RefreshTokenCleanupIntervalMinutes * 60000, stoppingToken);
        }
    }
}