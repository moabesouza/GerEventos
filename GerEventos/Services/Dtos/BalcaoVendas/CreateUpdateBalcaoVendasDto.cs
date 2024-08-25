using GerEventos.Services.Dtos.Eventos;
using System.ComponentModel.DataAnnotations;

namespace GerEventos.Services.Dtos.BalcaoVendas
{
    public class CreateUpdateBalcaoVendasDto
    {
        [Required]
        [StringLength(128)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        [Display(Name = "Localização")]
        public string Localizacao { get; set; } = string.Empty;

    }
}
