using System;
using System.Threading.Tasks;
using GerEventos.Services.BalcaoDeVendas;
using GerEventos.Services.Dtos.BalcaoVendas;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace GerEventos.Pages.BalcaoVendas
{
    public class EditModalModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBalcaoVendasDto BalcaoVendas { get; set; }

        private readonly IBalcaoVendasAppService _balcaoVendasAppService;

        public EditModalModel(IBalcaoVendasAppService balcaoVendasAppService)
        {
            _balcaoVendasAppService = balcaoVendasAppService;
        }

        public async Task OnGetAsync()
        {
            var balcaoVendasDto = await _balcaoVendasAppService.GetAsync(Id);
            BalcaoVendas = ObjectMapper.Map<BalcaoVendasDto, CreateUpdateBalcaoVendasDto>(balcaoVendasDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _balcaoVendasAppService.UpdateAsync(Id, BalcaoVendas);
            return NoContent();
        }
    }
}
