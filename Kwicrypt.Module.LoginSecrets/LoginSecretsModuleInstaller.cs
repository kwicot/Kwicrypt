using Kwicrypt.Module.Core;
using Kwicrypt.Module.LoginSecrets.Constants;
using Kwicrypt.Module.LoginSecrets.Factorys;
using Kwicrypt.Module.LoginSecrets.Interfaces;
using Kwicrypt.Module.LoginSecrets.Persistent;
using Kwicrypt.Module.LoginSecrets.Repositorys;
using Kwicrypt.Module.LoginSecrets.Services;
using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.LoginSecrets;

public class LoginSecretsModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<ILoginSecretFactory, LoginSecretFactory>();
        services.AddScoped<ILoginSecretRepository, LoginSecretRepository>();

        services.AddScoped<ILoginSecretsService, LoginSecretsService>();
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