using Kwicrypt.Module.Core;
using Kwicrypt.Module.LoginSecrets.Constants;
using Kwicrypt.Module.LoginSecrets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kwicrypt.Module.LoginSecrets.Persistent;

public class LoginSecretDbContextFactory : IDesignTimeDbContextFactory<LoginSecretDbContext>
{
    public LoginSecretDbContext CreateDbContext(string[] args) =>
        new (LoginSecretDbContext.CreateOptions(Paths.DB_CONTEXT_LOGIN_SECRETS));
}
public class LoginSecretDbContext : DBContextBase<LoginSecretDbContext,LoginSecret>
{
    public LoginSecretDbContext(DbContextOptions<LoginSecretDbContext> options) : base(options)
    {
    }
}