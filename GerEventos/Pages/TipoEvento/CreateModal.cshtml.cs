using System.Threading.Tasks;
using GerEventos.Services.TipoEventos;
using GerEventos.Services.Dtos.TipoEventos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using GerEventos.Entities;

namespace GerEventos.Pages.TipoEventos
{
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty]
        public CreateUpdateTipoEventoDto TipoEvento { get; set; }

        private readonly ITipoEventoAppService _tipoEventoAppService;

        public CreateModalModel(ITipoEventoAppService tipoEventoAppService)
        {
            _tipoEventoAppService = tipoEventoAppService;
        }

        public void OnGet()
        {
            TipoEvento = new CreateUpdateTipoEventoDto();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _tipoEventoAppService.CreateAsync(TipoEvento);
            return NoContent();
        }
    }
}
