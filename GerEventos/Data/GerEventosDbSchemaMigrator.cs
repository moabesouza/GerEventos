using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GerEventos.Data;

public class GerEventosDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public GerEventosDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the GerEventosDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<GerEventosDbContext>()
            .Database
            .MigrateAsync();

    }
}
