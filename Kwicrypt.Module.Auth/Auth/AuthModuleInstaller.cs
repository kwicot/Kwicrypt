using Backend.Modules.Auth.Factorys;
using Backend.Modules.Auth.Interfaces;
using Backend.Modules.Auth.Models;
using Backend.Modules.Auth.Repositorys;
using Backend.Modules.Auth.Services;
using Backend.Modules.Auth.Services.Background;
using Backend.Token;

namespace Backend.Modules.Auth;

public class AuthModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        
        services.AddScoped<IRefreshTokenFactory, RefreshTokenFactory>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<UserAuthService>();
        services.AddHostedService<ExpiredRefreshTokenCleanupBackgroundService>();
    }

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.JwtSettings.json", optional: false, reloadOnChange: false);
        
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
    }
}