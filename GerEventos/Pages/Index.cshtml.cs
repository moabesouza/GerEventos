using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GerEventos.Services;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using GerEventos.Services.Contador;

namespace GerEventos.Pages
{
    public class IndexModel : AbpPageModel
    {
        private readonly IContadorAppService _contadorAppService;

        public IndexModel(IContadorAppService contadorAppService)
        {
            _contadorAppService = contadorAppService;
        }

        public int EventCount { get; set; }
        public int BalcaoCount { get; set; }
        public int ProdutorCount { get; set; }

        public async Task OnGetAsync()
        {
            EventCount = await _contadorAppService.GetEventCountAsync();
            BalcaoCount = await _contadorAppService.GetBalcaoCountAsync();
            ProdutorCount = await _contadorAppService.GetProdutorCountAsync();
        }
    }
}
