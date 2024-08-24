using GerEventos.Entities;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Services.Dtos.BalcaoVendas
{
    public class BalcaoVendasDto : AuditedEntityDto<Guid>
    {
        public string Nome { get; set; }
        public string Localizacao { get; set; }
    }
}
