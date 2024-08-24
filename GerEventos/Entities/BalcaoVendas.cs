using GerEventos.Entities;
using Volo.Abp.Domain.Entities.Auditing;

public class BalcaoVendas : AuditedAggregateRoot<Guid>
{
    public string Nome { get; set; }
    public string Localizacao { get; set; }
    public int Status { get; set; } = (int)StatusEnum.Ativado;
    public virtual ICollection<Evento> Eventos { get; set; } = new HashSet<Evento>();
}