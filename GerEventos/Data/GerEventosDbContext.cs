using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using GerEventos.Entities;

namespace GerEventos.Data;

public class GerEventosDbContext : AbpDbContext<GerEventosDbContext>
{
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<TipoEvento> Tipo_Evento { get; set; }
    public DbSet<BalcaoVendas> Balcao_Vendas { get; set; }
    public DbSet<Produtor> Produtores { get; set; }

    public const string DbTablePrefix = "App";
    public const string DbSchema = null;

    public GerEventosDbContext(DbContextOptions<GerEventosDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();


        builder.Entity<TipoEvento>(b =>
        {
            b.ToTable(DbTablePrefix + "TipoEvento", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Nome).IsRequired().HasMaxLength(128);
            b.Property(x => x.Status).IsRequired();
        });


        builder.Entity<BalcaoVendas>(b =>
        {
            b.ToTable(DbTablePrefix + "BalcaoVendas", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Nome).IsRequired().HasMaxLength(128);
            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.Localizacao).IsRequired().HasMaxLength(256);
        });

        builder.Entity<Produtor>(b =>
        {
            b.ToTable(DbTablePrefix + "Produtor", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Nome).IsRequired().HasMaxLength(128);
            b.Property(x => x.Endereco).HasMaxLength(256);
            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.Site).HasMaxLength(128);
        });

        builder.Entity<Evento>(b =>
        {
            b.ToTable(DbTablePrefix + "Evento", DbSchema);
            b.ConfigureByConvention();
            b.Property(e => e.Valor).HasColumnType("decimal(18,2)");
            b.Property(x => x.Nome).IsRequired().HasMaxLength(128);

            b.HasOne(x => x.Produtor)
                .WithMany(p => p.Eventos)
                .HasForeignKey(x => x.ProdutorId)
                .IsRequired();

            b.HasOne(x => x.TipoEvento)
                .WithMany(te => te.Eventos)
                .HasForeignKey(x => x.TipoEventoId)
                .IsRequired();

            b.HasOne(x => x.BalcaoVendas)
                .WithMany(bv => bv.Eventos)
                .HasForeignKey(x => x.BalcaoVendasId)
                .IsRequired();
        });

    }

}


