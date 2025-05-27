using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Kwicrypt.Module.Core;

public interface IModuleInstaller
{
    public void Install(WebApplicationBuilder builder);
    
    public void Configure(WebApplicationBuilder builder);

    public void AddControllers<T>(WebApplicationBuilder builder, ApplicationPartManager apm) where T : IModuleInstaller
    {
        var assembly = typeof(T).Assembly;
        if (!apm.ApplicationParts.Any(p => p.Name == assembly.GetName().Name))
        {
            apm.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }
}