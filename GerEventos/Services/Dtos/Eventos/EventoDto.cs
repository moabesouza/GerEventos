
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Services.Dtos.Eventos
{
    public class EventoDto : AuditedEntityDto<Guid>
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Valor { get; set; }

        public string Produtor { get; set; }
        public string Endereco { get; set; }
        public string Site { get; set; }
        public Guid TipoEventoId { get; set; }
        public Guid BalcaoVendasId { get; set; }
    }
}


