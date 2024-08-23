using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions.Common;
using GerEventos.Data;
using Serilog;
using Serilog.Events;
using Volo.Abp.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GerEventos
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Configuração do Serilog
                builder.Host.AddAppSettingsSecretsJson()
                    .UseAutofac()
                    .UseSerilog((context, services, loggerConfiguration) =>
                    {
                        loggerConfiguration
#if DEBUG
                            .MinimumLevel.Debug()
#else
                            .MinimumLevel.Information()
#endif
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                            .Enrich.FromLogContext()
                            .WriteTo.Async(c => c.File("Logs/logs.txt"))
                            .WriteTo.Async(c => c.Console());

                        if (IsMigrateDatabase(args))
                        {
                            loggerConfiguration.MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning);
                            loggerConfiguration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                        }
                        else
                        {
                            loggerConfiguration.WriteTo.Async(c => c.AbpStudio(services));
                        }
                    });

                // Adicionando serviços de localização
                builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

                // Adicionando e configurando a aplicação
                await builder.AddApplicationAsync<GerEventosModule>();
                var app = builder.Build();
                await app.InitializeApplicationAsync();

                if (IsMigrateDatabase(args))
                {
                    await app.Services.GetRequiredService<GerEventosDbMigrationService>().MigrateAsync();
                    return 0;
                }

                Log.Information("Starting GerEventos.");
                await app.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                if (ex is HostAbortedException)
                {
                    throw;
                }

                Log.Fatal(ex, "GerEventos terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static bool IsMigrateDatabase(string[] args)
        {
            return args.Any(x => x.Contains("--migrate-database", StringComparison.OrdinalIgnoreCase));
        }
    }
}
