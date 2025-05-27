using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Backend.Modules;

public interface IModuleInstaller
{
    public void Install(WebApplicationBuilder builder);
    
    public void Configure(WebApplicationBuilder builder);

    public void AddControllers<T>(WebApplicationBuilder builder) where T : IModuleInstaller
    {
        var services = builder.Services;
        var assembly = typeof(T).Assembly;

        services.AddControllers()
            .ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts.Add(new AssemblyPart(assembly));
            });
    }
}