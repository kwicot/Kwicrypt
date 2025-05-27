namespace Backend.Modules;

public static class ModuleInstaller
{
    public static void InstallModule<T>(WebApplicationBuilder builder) where T : IModuleInstaller, new()
    {
        var installer = new T();
        
        installer.Configure(builder);
        installer.Install(builder);
        installer.AddControllers<T>(builder);
    }
}