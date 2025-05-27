using Backend.DBContext.Persistence;
using Backend.Modules.Telegram.Interfaces;
using Backend.Modules.Telegram.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.Telegram.Repositorys;

public class TelegramUserRepository : ITelegramUserRepository
{
    private readonly AppDbContext _context;

    public TelegramUserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddUserAsync(TelegramUser user)
    {
        _context.TelegramUsers.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(TelegramUser user)
    {
        _context.TelegramUsers.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<TelegramUser> GetByUserIdAsync(Guid userId) =>
        await _context.TelegramUsers.FirstOrDefaultAsync((user => user.UserId == userId));

    public async Task<TelegramUser> GetByChatIdAsync(long chatId) =>
        await _context.TelegramUsers.FirstOrDefaultAsync((user => user.ChatId == chatId));

    public bool HasId(Guid id) =>
        _context.TelegramUsers.Any(user => user.UserId == id);
}