using Backend.Modules.Data.Factorys;
using Backend.Modules.Data.Interfaces;
using Backend.Modules.Data.Repositorys;

namespace Backend.Modules.Data;

public class UsersModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services.AddScoped<IUserFactory, UserFactory>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public void Configure(WebApplicationBuilder builder) { }
}