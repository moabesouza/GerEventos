using Volo.Abp.Domain.Entities.Auditing;

namespace GerEventos.Entities
{
    public class Produtor : AuditedAggregateRoot<Guid>
    {
        public string Nome { get; set; }

        public string? Endereco { get; set; }
        public string? Site { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Ativado;

        public virtual ICollection<Evento> Eventos { get; set; } = new HashSet<Evento>();
    }
}
