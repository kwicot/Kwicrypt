using Backend.Modules.Telegram.Models;

namespace Backend.Modules.Telegram.Interfaces;

public interface ITelegramUserRepository
{
    public Task AddUserAsync(TelegramUser user);
    public Task DeleteUserAsync (TelegramUser user);
    public Task<TelegramUser> GetByUserIdAsync(Guid userId);
    public Task<TelegramUser> GetByChatIdAsync(long chatId);
    public bool HasId (Guid id);
}