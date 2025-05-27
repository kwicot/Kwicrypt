using Kwicrypt.Module.Auth.Constants;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kwicrypt.Module.Auth.Persistent;

public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args) =>
        new (UserDbContext.CreateOptions(Constants.Paths.DB_CONTEXT_USERS));
}
public class UserDbContext : DBContextBase<UserDbContext,User>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
}