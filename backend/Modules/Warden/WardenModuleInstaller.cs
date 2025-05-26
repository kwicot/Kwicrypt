using Backend.Modules.Warden.Factorys;
using Backend.Modules.Warden.Interfaces;
using Backend.Modules.Warden.Repositorys;

namespace Backend.Modules.Warden;

public class WardenModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddScoped<ILoginDataFactory, LoginDataFactory>();
        services.AddScoped<ILoginDataRepository, LoginDataRepository>();
    }

    public void Configure(WebApplicationBuilder builder) { }
}