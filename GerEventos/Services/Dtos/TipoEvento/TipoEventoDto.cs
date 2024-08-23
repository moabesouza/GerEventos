using GerEventos.Entities;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Services.Dtos.TipoEvento
{
    public class TipoEventoDto : AuditedEntityDto<Guid>
    {
        public string Nome { get; set; }
     
        public StatusEnum Status { get; set; }
    }
}
