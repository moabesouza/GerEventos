using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class Venda : AuditedAggregateRoot<Guid>
    {
        [Required]
        [Display(Name = "Ingresso")]
        public Guid IngressoId { get; set; }

        [ForeignKey(nameof(IngressoId))]
        public Ingresso Ingresso { get; set; } = new Ingresso();

        [Required]
        [Display(Name = "Balcão de Vendas")]
        public Guid BalcaoVendasId { get; set; }

        [Required]
        [Display(Name = "Data da Venda")]
        public DateTime DataVenda { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser zero ou positivo.")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required]
        [Display(Name = "Meio de Pagamento")]
        public MeioPagamentoEnum MeioPagamento { get; set; }
    }
}
