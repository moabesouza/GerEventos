using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GerEventos.Data;

public class GerEventosDbContextFactory : IDesignTimeDbContextFactory<GerEventosDbContext>
{
    public GerEventosDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<GerEventosDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new GerEventosDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}