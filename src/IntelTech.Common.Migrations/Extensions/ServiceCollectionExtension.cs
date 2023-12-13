using IntelTech.Common.Migrations.Migrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Common.Migrations.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDbMigrator<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
        => services.AddScoped<IDbMigrator, DbMigrator<TDbContext>>();
}
