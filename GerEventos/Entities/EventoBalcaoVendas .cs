using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class EventoBalcaoVendas : AuditedAggregateRoot<Guid>
    {
        [Required]
        [Display(Name = "Evento")]
        public Guid EventoId { get; set; }

        [ForeignKey(nameof(EventoId))]
        public Evento Evento { get; set; } = new Evento();

        [Required]
        [Display(Name = "Balcão de Vendas")]
        public Guid BalcaoVendasId { get; set; }

        [ForeignKey(nameof(BalcaoVendasId))]
        public BalcaoVendas BalcaoVendas { get; set; } = new BalcaoVendas();
    }
}
