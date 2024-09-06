using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace GerEventos.Entities
{
    public class UsuarioBalcaoVendas : AuditedAggregateRoot<Guid>
    {
        [Required]
        [Display(Name = "Usuário")]
        public Guid UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public IdentityUser<Guid> Usuario { get; set; } = new IdentityUser<Guid>();

        [Required]
        [Display(Name = "Balcão de Vendas")]
        public Guid BalcaoVendasId { get; set; }

        [ForeignKey(nameof(BalcaoVendasId))]
        public BalcaoVendas BalcaoVendas { get; set; } = new BalcaoVendas();

       
    }
}
