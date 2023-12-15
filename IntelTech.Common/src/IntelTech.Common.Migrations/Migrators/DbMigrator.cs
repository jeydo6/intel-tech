using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IntelTech.Common.Migrations.Migrators;

public interface IDbMigrator
{
    Task MigrateAsync();

    void Migrate();
}

internal sealed class DbMigrator<TDbContext> : IDbMigrator
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public DbMigrator(TDbContext dbContext)
        => _dbContext = dbContext;

    public void Migrate()
    {
        var migrations = _dbContext.Database.GetPendingMigrations();
        if (migrations.Any())
            _dbContext.Database.Migrate();
    }

    public async Task MigrateAsync()
    {
        var migrations = await _dbContext.Database.GetPendingMigrationsAsync();
        if (migrations.Any())
            await _dbContext.Database.MigrateAsync();
    }
}
