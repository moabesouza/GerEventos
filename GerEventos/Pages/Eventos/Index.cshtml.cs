using GerEventos.Services.Dtos.Eventos;
using GerEventos.Services.Dtos.Produtor;
using GerEventos.Services.Eventos;
using GerEventos.Services.Produtores;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly IEventoAppService _eventoAppService;
    private readonly IProdutorAppService _produtorAppService;

    public IndexModel(IEventoAppService eventoAppService, IProdutorAppService produtorAppService)
    {
        _eventoAppService = eventoAppService;
        _produtorAppService = produtorAppService;
    }

    //public IList<ProdutorDto> Produtores { get; set; }
    //public EventoDto Evento { get; set; } = new EventoDto();
    public FilterEventoDto FiltroEvento { get; set; } = new FilterEventoDto();
    //public bool CanCreate { get; set; }

    public async Task OnGetAsync()
    {
        //var produtoresResult = await _produtorAppService.GetSelectProdutorAsync();
        //Produtores = produtoresResult.Items.ToList(); 

    }
}
