using GerEventos.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

public class Evento : AuditedAggregateRoot<Guid>
{
 
    public string Nome { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }

    public Guid BalcaoVendasId { get; set; }
    public Guid ProdutorId { get; set; }
    public Guid TipoEventoId { get; set; }

    [ForeignKey("TipoEventoId")]
    public virtual TipoEvento TipoEvento { get; set; } = null!;
    [ForeignKey("BalcaoVendasId")]
    public virtual BalcaoVendas BalcaoVendas { get; set; } = null!;
    [ForeignKey("ProdutorId")]
    public virtual Produtor Produtor { get; set; } = null!;
}