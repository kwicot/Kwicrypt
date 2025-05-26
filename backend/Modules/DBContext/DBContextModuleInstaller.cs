using Backend.DBContext.Persistence;
using Backend.Modules.DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Modules.DBContext;

public class DBContextModuleInstaller : IModuleInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite("Data Source=../Data/main.db");
            options.LogTo(_ => { }, LogLevel.Warning);
        });
    }

    public void Configure(WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.DbSettings.json", optional: false, reloadOnChange: false);
        builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
    }
}