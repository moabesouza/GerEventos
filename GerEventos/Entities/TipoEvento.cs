using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class TipoEvento : AuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(128, ErrorMessage = "O nome do tipo de evento deve ter no máximo 128 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Status")]
        public StatusEnum Status { get; set; }

        public ICollection<Evento> Eventos { get; set; } = new HashSet<Evento>();
    }
}
