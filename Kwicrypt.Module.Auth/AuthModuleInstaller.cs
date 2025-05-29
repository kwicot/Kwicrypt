using Kwicrypt.Module.Auth.Factorys;
using Kwicrypt.Module.Auth.Interfaces;
using Kwicrypt.Module.Auth.Models;
using Kwicrypt.Module.Auth.Persistent;
using Kwicrypt.Module.Auth.Repositorys;
using Kwicrypt.Module.Auth.Services;
using Kwicrypt.Module.Auth.Services.Background;
using Kwicrypt.Module.Core;

#if MODULE_CRYPTO
using Kwicrypt.Module.Cryptography.Interfaces;
using Kwicrypt.Module.Cryptography.Services;
#endif

using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Auth;

public class AuthModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        
        services.AddScoped<IRefreshTokenFactory, RefreshTokenFactory>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        services.AddScoped<ITokenService, TokenService>();
        
        services.AddScoped<IUserFactory, UserFactory>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<UserAuthService>();
        
#if MODULE_CRYPTO
        services.AddSingleton<ICryptoService, CryptoService>();
#endif
        
        services.AddHostedService<ExpiredRefreshTokenCleanupBackgroundService>();
    }

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.JwtSettings.json", optional: false, reloadOnChange: false);
        
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        
        //DB contexts
        builder.Services.AddDbContext<UserDbContext>(options =>
        {
            var optionsFromMethod = UserDbContext.CreateOptions(Constants.Paths.DB_CONTEXT_USERS);
            options.UseSqlite(optionsFromMethod.Extensions.OfType<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>().First().ConnectionString);
        });
        
        builder.Services.AddDbContext<RefreshTokenDbContext>(options =>
        {
            var optionsFromMethod = RefreshTokenDbContext.CreateOptions(Constants.Paths.DB_CONTEXT_REFRESH_TOKENS);
            options.UseSqlite(optionsFromMethod.Extensions.OfType<Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal.SqliteOptionsExtension>().First().ConnectionString);
        });
    }
}