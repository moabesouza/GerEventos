using System.Threading.Tasks;
using GerEventos.Services.BalcaoDeVendas;
using GerEventos.Services.Dtos.BalcaoVendas;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace GerEventos.Pages.BalcaoVendas
{
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty]
        public CreateUpdateBalcaoVendasDto BalcaoVendas { get; set; }

        private readonly IBalcaoVendasAppService _balcaoVendasAppService;

        public CreateModalModel(IBalcaoVendasAppService balcaoVendasAppService)
        {
            _balcaoVendasAppService = balcaoVendasAppService;
        }

        public void OnGet()
        {
            BalcaoVendas = new CreateUpdateBalcaoVendasDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _balcaoVendasAppService.CreateAsync(BalcaoVendas);
            return NoContent();
        }
    }
}
