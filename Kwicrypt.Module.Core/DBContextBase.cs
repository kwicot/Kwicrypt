using Microsoft.EntityFrameworkCore;

namespace Kwicrypt.Module.Core;

public abstract class DBContextBase<TContext, TEntity> : DbContext
    where TContext : DbContext
    where TEntity : DbModelBase
{
    public DbSet<TEntity> List { get; set; }

    public DBContextBase(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    
    public static DbContextOptions<TContext> CreateOptions(string relativeDbPath, string migrationsAssembly = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();

        var dbPath = Path.GetFullPath(relativeDbPath);
        var fileInfo = new FileInfo(dbPath);

        if (!Directory.Exists(fileInfo.DirectoryName))
            Directory.CreateDirectory(fileInfo.DirectoryName);

        migrationsAssembly ??= typeof(TContext).Assembly.GetName().Name;

        optionsBuilder.UseSqlite(
            $"Data Source={dbPath}",
            b => b.MigrationsAssembly(migrationsAssembly)
        );

        return optionsBuilder.Options;
    }

    public async Task<Guid> GetId()
    {
        var id = Guid.NewGuid();
        if (await HaveId(id))
            return await GetId();

        return id;
    }

    public async Task<bool> HaveId(Guid id) =>
         await List.AnyAsync(x => x.Id == id);
}