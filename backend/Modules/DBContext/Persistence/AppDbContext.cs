using Backend.Modules.Auth.Models;
using Backend.Modules.Data.Models;
using Backend.Modules.Telegram.Models;
using Backend.Modules.Warden.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DBContext.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<TelegramLinkToken> TelegramLinkTokens { get; set; }
    public DbSet<TelegramUser> TelegramUsers { get; set; }
    public DbSet<LoginData> LoginDatas { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Token).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }

    public async Task<Guid> GetId()
    {
        var id = Guid.NewGuid();
        if (await HaveId(id))
            return await GetId();

        return id;
    }

    public async Task<bool> HaveId(Guid id)
    {
        var haveUser = await Users.AnyAsync(x => x.Id == id);
        var haveRefreshToken = await RefreshTokens.AnyAsync(x => x.Id == id);
        var haveTelegramUser = await TelegramUsers.AnyAsync(x => x.Id == id);
        
        return (!haveUser && !haveRefreshToken && !haveTelegramUser);
    }
}