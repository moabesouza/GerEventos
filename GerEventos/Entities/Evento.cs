using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class Evento : AuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(100, ErrorMessage = "O nome do evento deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "A descrição do evento deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "A localização deve ter no máximo 200 caracteres.")]
        [Display(Name = "Localização")]
        public string? Localizacao { get; set; }

        [Required]
        [Display(Name = "Data Início")]
        public DateTime DataInicio { get; set; }

        [Required]
        [Display(Name = "Data Fim")]
        public DateTime DataFim { get; set; }

        [Required]
        [Display(Name = "Tipo de Evento")]
        public Guid TipoEventoId { get; set; }

        [ForeignKey(nameof(TipoEventoId))]
        public TipoEvento TipoEvento { get; set; } = new TipoEvento();

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser um número positivo.")]
        [Display(Name = "Capacidade")]
        public int Capacidade { get; set; }

        [Required]
        [Display(Name = "Status")]
        public StatusEnum Status { get; set; }

        public ICollection<Ingresso> Ingressos { get; set; } = new HashSet<Ingresso>();
        public ICollection<EventoProdutor> EventoProdutores { get; set; } = new HashSet<EventoProdutor>();
        public ICollection<EventoBalcaoVendas> EventoBalcaoVendas { get; set; } = new HashSet<EventoBalcaoVendas>();
    }
}
