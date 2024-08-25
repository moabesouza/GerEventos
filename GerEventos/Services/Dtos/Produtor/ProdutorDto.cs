using GerEventos.Entities;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Services.Dtos.Produtor
{
    public class ProdutorDto : AuditedEntityDto<Guid>
    {
        public string Nome { get; set; }

        public string? Endereco { get; set; }
        public string? Site { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Ativado;
    }
}
