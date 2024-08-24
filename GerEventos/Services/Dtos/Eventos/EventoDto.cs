
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Services.Dtos.Eventos
{
    public class EventoDto : AuditedEntityDto<Guid>
    {
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public decimal Valor { get; set; }

        public string Produtor { get; set; }
        public string Endereco { get; set; }
        public string Site { get; set; }
        public Guid TipoEventoId { get; set; }
        public Guid BalcaoVendasId { get; set; }
        public string? NomeBalcaoVendas { get; set; }
        public string? NomeTipoEvento { get; set; }
    }
}


