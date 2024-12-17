using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UniqueDraw.Infrastructure.Adapters.Persistence.EFContext;

namespace UniqueDraw.Api;

public class AplicationContextFactory : IDesignTimeDbContextFactory<UniqueDrawDbContext>
{
    public UniqueDrawDbContext CreateDbContext(string[] args)
    {
        var Config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UniqueDrawDbContext>();
        optionsBuilder.UseSqlServer(Config.GetConnectionString("DefaultConnection"), sqlopts =>
        {
            sqlopts.MigrationsHistoryTable("_MigrationHistory", Config.GetValue<string>("SchemaName"));
        });

        return new UniqueDrawDbContext(optionsBuilder.Options);
    }
}
