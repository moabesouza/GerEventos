using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;


namespace GerEventos.Entities
{
    public class BalcaoVendas : AuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(100, ErrorMessage = "O nome do balcão de vendas deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "A localização deve ter no máximo 200 caracteres.")]
        [Display(Name = "Localização")]
        public string? Localizacao { get; set; }

        [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        [Phone(ErrorMessage = "O telefone fornecido não é válido.")]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [Required]
        [Display(Name = "Status")]
        public StatusEnum Status { get; set; }

        public ICollection<EventoBalcaoVendas> EventoBalcaoVendas { get; set; } = new HashSet<EventoBalcaoVendas>();
    }
}
