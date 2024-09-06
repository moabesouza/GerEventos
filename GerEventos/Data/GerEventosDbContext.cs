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

namespace GerEventos.Data
{
    public class GerEventosDbContext : AbpDbContext<GerEventosDbContext>
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<TipoEvento> TipoEvento { get; set; }
        public DbSet<BalcaoVendas> BalcaoVendas { get; set; }
        public DbSet<Produtor> Produtores { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<UsuarioBalcaoVendas> UsuarioBalcaoVendas { get; set; }
        public DbSet<EventoProdutor> EventoProdutores { get; set; }
        public DbSet<EventoBalcaoVendas> EventoBalcaoVendas { get; set; }

        public const string DbTablePrefix = "App";
        public const string DbSchema = null;

        public GerEventosDbContext(DbContextOptions<GerEventosDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Include modules to your migration db context
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureFeatureManagement();
            builder.ConfigurePermissionManagement();
            builder.ConfigureBlobStoring();
            builder.ConfigureIdentity();
            builder.ConfigureOpenIddict();
            builder.ConfigureTenantManagement();

            // Configurações para TipoEvento
            builder.Entity<TipoEvento>(b =>
            {
                b.ToTable(DbTablePrefix + "TipoEvento", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Nome)
                    .IsRequired()
                    .HasMaxLength(128);
                b.Property(x => x.Status)
                    .IsRequired();
            });

            // Configurações para BalcaoVendas
            builder.Entity<BalcaoVendas>(b =>
            {
                b.ToTable(DbTablePrefix + "BalcaoVendas", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Nome)
                    .IsRequired()
                    .HasMaxLength(128);
                b.Property(x => x.Status)
                    .IsRequired();
                b.Property(x => x.Localizacao)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            // Configurações para Produtor
            builder.Entity<Produtor>(b =>
            {
                b.ToTable(DbTablePrefix + "Produtor", DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Nome)
                    .IsRequired()
                    .HasMaxLength(128);
                b.Property(x => x.Endereco)
                    .HasMaxLength(256);
                b.Property(x => x.Status)
                    .IsRequired();
                b.Property(x => x.Site)
                    .HasMaxLength(128);
                b.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);
                b.Property(x => x.Telefone)
                    .HasMaxLength(15);
            });

            // Configurações para Evento
            builder.Entity<Evento>(b =>
            {
                b.ToTable(DbTablePrefix + "Evento", DbSchema);
                b.ConfigureByConvention();
                b.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(128);
                b.Property(e => e.Descricao)
                    .HasMaxLength(500);
                b.Property(e => e.Localizacao)
                    .IsRequired()
                    .HasMaxLength(300);
                b.Property(e => e.DataInicio)
                    .IsRequired();
                b.Property(e => e.DataFim)
                    .IsRequired();
                b.Property(e => e.Capacidade)
                    .IsRequired();
            
                b.Property(e => e.Status)
                    .IsRequired();

                b.HasOne(x => x.TipoEvento)
                    .WithMany(te => te.Eventos)
                    .HasForeignKey(x => x.TipoEventoId)
                    .IsRequired();

    
            });

            // Configurações para Ingresso
            builder.Entity<Ingresso>(b =>
            {
                b.ToTable(DbTablePrefix + "Ingresso", DbSchema);
                b.ConfigureByConvention();
                b.Property(i => i.Valor)
                    .HasColumnType("decimal(18,2)");

                b.HasOne(i => i.Evento)
                    .WithMany(e => e.Ingressos)
                    .HasForeignKey(i => i.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurações para Venda
            builder.Entity<Venda>(b =>
            {
                b.ToTable(DbTablePrefix + "Venda", DbSchema);
                b.ConfigureByConvention();
                b.Property(v => v.DataVenda)
                    .IsRequired();
                b.Property(v => v.Valor)
                    .HasColumnType("decimal(18,2)");
                b.Property(v => v.MeioPagamento)
                    .IsRequired();

                b.HasOne(v => v.Ingresso)
                    .WithMany(i => i.Vendas)
                    .HasForeignKey(v => v.IngressoId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            // Configurações para UsuarioBalcaoVendas
            builder.Entity<UsuarioBalcaoVendas>(b =>
            {
                b.ToTable(DbTablePrefix + "UsuarioBalcaoVendas", DbSchema);
                b.ConfigureByConvention();

                b.HasOne(ubv => ubv.Usuario)
                    .WithMany() // Assumindo que a navegação inversa está configurada no User do ABP
                    .HasForeignKey(ubv => ubv.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(ubv => ubv.BalcaoVendas)
                    .WithMany()
                    .HasForeignKey(ubv => ubv.BalcaoVendasId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configurações para EventoProdutor
            builder.Entity<EventoProdutor>(b =>
            {
                b.ToTable(DbTablePrefix + "EventoProdutor", DbSchema);
                b.ConfigureByConvention();

                b.HasKey(ep => new { ep.EventoId, ep.ProdutorId });

                b.HasOne(ep => ep.Evento)
                    .WithMany(e => e.EventoProdutores)
                    .HasForeignKey(ep => ep.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(ep => ep.Produtor)
                    .WithMany(p => p.EventoProdutores)
                    .HasForeignKey(ep => ep.ProdutorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configurações para EventoBalcaoVendas
            builder.Entity<EventoBalcaoVendas>(b =>
            {
                b.ToTable(DbTablePrefix + "EventoBalcaoVendas", DbSchema);
                b.ConfigureByConvention();

                b.HasKey(eb => new { eb.EventoId, eb.BalcaoVendasId });

                b.HasOne(eb => eb.Evento)
                    .WithMany(e => e.EventoBalcaoVendas)
                    .HasForeignKey(eb => eb.EventoId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(eb => eb.BalcaoVendas)
                    .WithMany(b => b.EventoBalcaoVendas)
                    .HasForeignKey(eb => eb.BalcaoVendasId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
