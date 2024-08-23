using GerEventos.Services.Dtos.Eventos;
using System.ComponentModel.DataAnnotations;

namespace GerEventos.Services.Dtos.BalcaoVendas
{
    public class CreateUpdateBalcaoVendasDto
    {
        [Required]
        [StringLength(128)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        public string Localizacao { get; set; } = string.Empty;

    }
}
