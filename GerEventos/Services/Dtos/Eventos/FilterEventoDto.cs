using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

public class FilterEventoDto : PagedAndSortedResultRequestDto
{
    [Display(Name = "Nome")]
    public string? Nome { get; set; }
    [Display(Name = "Período")]
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    [Display(Name = "Produtor")]
    public Guid? ProdutorId { get; set; }
}
