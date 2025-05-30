using System.Text;
using Kwicrypt.Module.Auth;
using Kwicrypt.Module.Core;
using Kwicrypt.Module.LoginSecrets;
using Kwicrypt.Module.LoginSecrets.Persistent;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.IdentityModel.Tokens;

namespace Kwicrypt.Backend;

public class Program
{
    public static void Main(string[] args)
    {
        Dictionary<string, string> parsedArgs = ParseArguments(args);

        WebApplicationBuilder _builder = InitializeBuilder();
        WebApplication _app = InitializeApp(_builder);


        _app.Run();
    }

    static WebApplicationBuilder InitializeBuilder(params string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //TODO добавить защиту
        services.AddDataProtection();
        
        var partManager = new ApplicationPartManager();
        partManager.ApplicationParts.Clear();

        ModuleInstaller.InstallModule<AuthModuleInstaller>(builder, partManager);
        ModuleInstaller.InstallModule<LoginSecretsModuleInstaller>(builder, partManager);
        
        
        //ModuleInstaller.InstallModule<TelegramModuleInstaller>(builder);
        
        builder.Services.AddControllers()
            .ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts.Clear();
                foreach (var part in partManager.ApplicationParts)
                    apm.ApplicationParts.Add(part);
            });

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                };
            });


        builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
        return builder;
    }

    static WebApplication InitializeApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }

    static Dictionary<string, string> ParseArguments(string[] args)
    {
        var result = new Dictionary<string, string>();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("-"))
            {
                var key = args[i];
                var value = i + 1 < args.Length && !args[i + 1].StartsWith("-") ? args[i + 1] : null;
                result[key] = value;
                i++;
            }
        }

        return result;
    }
}