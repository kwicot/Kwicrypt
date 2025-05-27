using Kwicrypt.Module.Core;
using Kwicrypt.Module.LoginSecrets.Constants;
using Kwicrypt.Module.LoginSecrets.Factorys;
using Kwicrypt.Module.LoginSecrets.Interfaces;
using Kwicrypt.Module.LoginSecrets.Persistent;
using Kwicrypt.Module.LoginSecrets.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.LoginSecrets;

public class WardenModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<ILoginSecretFactory, LoginSecretFactory>();
        services.AddScoped<ILoginDataRepository, LoginDataRepository>();
    }

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LoginSecretDbContext>(options =>
        {
            var optionsFromMethod = LoginSecretDbContext.CreateOptions(Paths.DB_CONTEXT_LOGIN_SECRETS);
            options.UseSqlite(optionsFromMethod.Extensions.OfType<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>().First().ConnectionString);
        });
    }
}