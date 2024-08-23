
using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class TipoEvento : AuditedAggregateRoot<Guid>
    {
        public string Nome { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Ativado;
        public virtual ICollection<Evento> Eventos { get; set; } = new HashSet<Evento>();
    }
}
