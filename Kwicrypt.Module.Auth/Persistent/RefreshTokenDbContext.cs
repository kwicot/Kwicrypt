using Kwicrypt.Module.Auth.Constants;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kwicrypt.Module.Auth.Persistent;

public class RefreshTokenDbContextFactory : IDesignTimeDbContextFactory<RefreshTokenDbContext>
{
    public RefreshTokenDbContext CreateDbContext(string[] args) =>
        new (RefreshTokenDbContext.CreateOptions(Constants.Paths.DB_CONTEXT_REFRESH_TOKENS));
}

public class RefreshTokenDbContext : DBContextBase<RefreshTokenDbContext,RefreshToken>
{
    public RefreshTokenDbContext(DbContextOptions<RefreshTokenDbContext> options) : base(options)
    {
    }
}