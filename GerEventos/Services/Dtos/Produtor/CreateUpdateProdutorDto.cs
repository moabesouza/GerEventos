using GerEventos.Entities;
using System.ComponentModel.DataAnnotations;

namespace GerEventos.Services.Dtos.Produtor
{
    public class CreateUpdateProdutorDto
    {
        [Required]
        [StringLength(128)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        [Display(Name = "Endereço")]
        public string? Endereco { get; set; } = string.Empty;

        [StringLength(128)]
        [Display(Name = "Site")]
        public string? Site { get; set; } = string.Empty;

    }
}
