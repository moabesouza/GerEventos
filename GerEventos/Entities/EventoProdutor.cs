using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class EventoProdutor : AuditedAggregateRoot<Guid>
    {
        [Required]
        [Display(Name = "Evento")]
        public Guid EventoId { get; set; }

        [ForeignKey(nameof(EventoId))]
        public Evento Evento { get; set; } = new Evento();

        [Required]
        [Display(Name = "Produtor")]
        public Guid ProdutorId { get; set; }

        [ForeignKey(nameof(ProdutorId))]
        public Produtor Produtor { get; set; } = new Produtor();
    }
}
