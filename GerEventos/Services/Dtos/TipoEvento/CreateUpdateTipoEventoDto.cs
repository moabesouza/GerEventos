using System.ComponentModel.DataAnnotations;

namespace GerEventos.Services.Dtos.TipoEventos
{
    public class CreateUpdateTipoEventoDto
    {
        [Required]
        [StringLength(128)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

    }
}
