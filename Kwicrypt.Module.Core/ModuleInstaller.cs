using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Kwicrypt.Module.Core;

public static class ModuleInstaller
{
    public static void InstallModule<T>(WebApplicationBuilder builder, ApplicationPartManager apm) where T : IModuleInstaller, new()
    {
        var installer = new T();
        
        installer.Configure(builder);
        installer.Install(builder);
        installer.AddControllers<T>(builder, apm);
    }
}