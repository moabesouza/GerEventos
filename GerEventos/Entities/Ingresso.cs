using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class Ingresso : AuditedAggregateRoot<Guid>
    {
        [Required]
        [Display(Name = "Evento")]
        public Guid EventoId { get; set; }

        [ForeignKey(nameof(EventoId))]
        public Evento Evento { get; set; } = new Evento();

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser zero ou positivo.")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required]
        [Display(Name = "Status")]
        public StatusEnum Status { get; set; }

        public ICollection<Venda> Vendas { get; set; } = new HashSet<Venda>();
    }
}
