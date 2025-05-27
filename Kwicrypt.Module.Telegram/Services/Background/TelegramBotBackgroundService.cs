using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Kwicrypt.Module.Telegram.Services.Background;

public class TelegramBotBackgroundService : BackgroundService
{
    private readonly ILogger<TelegramBotBackgroundService> _logger;
    private readonly TelegramSettings _settings;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private ITelegramLinkService _linkService;
    private TelegramBotClient _botClient;

    public TelegramBotBackgroundService(
        ILogger<TelegramBotBackgroundService> logger,
        IOptions<TelegramSettings> settings,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _settings = settings.Value;
        _serviceScopeFactory = serviceScopeFactory;
        
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _botClient = new TelegramBotClient(_settings.BotToken);
        _botClient.OnMessage += OnMessage;

        _logger.LogInformation("Telegram bot started and receiving updates.");

        // Keep the background service alive
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task OnMessage(Message message, UpdateType type)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        _linkService = scope.ServiceProvider.GetRequiredService<ITelegramLinkService>();
        
        if (message.Text.StartsWith("/start"))
        {
            var parts = message.Text.Split(' ');

            if (parts.Length < 2 || !Guid.TryParse(parts[1], out var tokenGuid))
            {
                await _botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Invalid or missing token.");
                return;
            }

            var token = await _linkService.VerifyTokenAsync(tokenGuid);
            if (token == null)
            {
                await _botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Token expired or invalid. Try again.");
                return;
            }

            var result = await _linkService.LinkUser(tokenGuid, message.Chat.Id, message.From?.Username);

            if (result == null)
            {
                await _botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Something went wrong. Please contact the administrator.");
            }
            else
            {
                await _botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Telegram link successfully.");
            }
        }
    }
}