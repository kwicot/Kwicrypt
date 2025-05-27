using Kwicrypt.Module.Telegram.Interfaces;
using Kwicrypt.Module.Telegram.Models;
using Kwicrypt.Module.Telegram.Persistent;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Telegram.Repositorys;

public class TelegramUserRepository : ITelegramUserRepository
{
    private readonly TelegramUserDbContext _context;

    public TelegramUserRepository(TelegramUserDbContext context)
    {
        _context = context;
    }
    
    public async Task AddUserAsync(TelegramUser user)
    {
        _context.List.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(TelegramUser user)
    {
        _context.List.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<TelegramUser> GetByUserIdAsync(Guid userId) =>
        await _context.List.FirstOrDefaultAsync((user => user.UserId == userId));

    public async Task<TelegramUser> GetByChatIdAsync(long chatId) =>
        await _context.List.FirstOrDefaultAsync((user => user.ChatId == chatId));

    public bool HasId(Guid id) =>
        _context.List.Any(user => user.UserId == id);
}