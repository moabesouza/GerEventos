using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace GerEventos.Data;

public class GerEventosDbMigrationService : ITransientDependency
{
    public ILogger<GerEventosDbMigrationService> Logger { get; set; }

    private readonly IDataSeeder _dataSeeder;
    private readonly GerEventosDbSchemaMigrator _dbSchemaMigrator;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;

    public GerEventosDbMigrationService(
        IDataSeeder dataSeeder,
        GerEventosDbSchemaMigrator dbSchemaMigrator,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant)
    {
        _dataSeeder = dataSeeder;
        _dbSchemaMigrator = dbSchemaMigrator;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;

        Logger = NullLogger<GerEventosDbMigrationService>.Instance;
    }

    public async Task MigrateAsync()
    {
        if (AddInitialMigrationIfNotExist())
        {
            return;
        }

        Logger.LogInformation("Started database migrations...");

        await MigrateAndSeedHostDatabaseAsync();

        var tenants = await _tenantRepository.GetListAsync(includeDetails: true);

        var migratedDatabaseSchemas = new HashSet<string>();
        foreach (var tenant in tenants)
        {
            await MigrateAndSeedTenantDatabaseAsync(tenant, migratedDatabaseSchemas);
        }

        Logger.LogInformation("Successfully completed all database migrations.");
        Logger.LogInformation("You can safely end this process...");
    }

    private async Task MigrateAndSeedHostDatabaseAsync()
    {
        Logger.LogInformation("Migrating and seeding host database...");
        await MigrateDatabaseSchemaAsync();
        await SeedDataAsync();
        Logger.LogInformation("Successfully completed host database migrations.");
    }

    private async Task MigrateAndSeedTenantDatabaseAsync(Tenant tenant, HashSet<string> migratedDatabaseSchemas)
    {
        using (_currentTenant.Change(tenant.Id))
        {
            if (tenant.ConnectionStrings.Any())
            {
                var tenantConnectionStrings = tenant.ConnectionStrings.Select(x => x.Value).ToList();

                if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                {
                    await MigrateDatabaseSchemaAsync(tenant);
                    migratedDatabaseSchemas.UnionWith(tenantConnectionStrings);
                }
            }

            await SeedDataAsync(tenant);
            Logger.LogInformation($"Successfully completed {tenant.Name} tenant database migrations.");
        }
    }

    private async Task MigrateDatabaseSchemaAsync(Tenant tenant = null)
    {
        var target = tenant == null ? "host" : $"{tenant.Name} tenant";
        Logger.LogInformation($"Migrating schema for {target} database...");
        await _dbSchemaMigrator.MigrateAsync();
    }

    private async Task SeedDataAsync(Tenant tenant = null)
    {
        var target = tenant == null ? "host" : $"{tenant.Name} tenant";
        Logger.LogInformation($"Executing {target} database seed...");
        await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
            .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
            .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue));
    }

    private bool AddInitialMigrationIfNotExist()
    {
        try
        {
            if (!DbMigrationsProjectExists())
            {
                return false;
            }

            if (!MigrationsFolderExists())
            {
                AddInitialMigration();
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Logger.LogWarning($"Failed to determine if initial migration is needed: {ex.Message}");
            return false;
        }
    }

    private bool DbMigrationsProjectExists()
    {
        return Directory.Exists(GetEntityFrameworkCoreProjectFolderPath());
    }

    private bool MigrationsFolderExists()
    {
        var migrationsPath = Path.Combine(GetEntityFrameworkCoreProjectFolderPath(), "Migrations");
        return Directory.Exists(migrationsPath);
    }

    private void AddInitialMigration()
    {
        Logger.LogInformation("Creating initial migration...");

        var (fileName, arguments) = GetMigrationProcessInfo();

        try
        {
            Process.Start(new ProcessStartInfo(fileName, arguments)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }
        catch (Exception ex)
        {
            Logger.LogError($"Failed to run ABP CLI for migration: {ex.Message}");
            throw new Exception("Couldn't run ABP CLI...", ex);
        }
    }

    private (string fileName, string arguments) GetMigrationProcessInfo()
    {
        string argumentPrefix = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "/C" : "-c";
        string fileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd.exe" : "/bin/bash";
        string projectPath = GetEntityFrameworkCoreProjectFolderPath();
        string arguments = $"{argumentPrefix} \"abp create-migration-and-run-migrator \"{projectPath}\" --nolayers\"";

        return (fileName, arguments);
    }

    private string GetEntityFrameworkCoreProjectFolderPath()
    {
        var slnDirectoryPath = GetSolutionDirectoryPath();
        if (slnDirectoryPath == null)
        {
            throw new Exception("Solution folder not found!");
        }

        return Path.Combine(slnDirectoryPath, "GerEventos");
    }

    private string GetSolutionDirectoryPath()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory?.Parent != null)
        {
            if (Directory.GetFiles(currentDirectory.FullName, "*.sln").Any())
            {
                return currentDirectory.FullName;
            }

            currentDirectory = currentDirectory.Parent;
        }

        return null;
    }
}
