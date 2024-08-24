using System.Threading.Tasks;
using GerEventos.Services.TipoEventos;
using GerEventos.Services.Dtos.TipoEventos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;


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
          
            if (await _tipoEventoAppService.nomeJaExiste(TipoEvento.Nome))
            {
                ModelState.AddModelError("TipoEvento.Nome", "O nome do tipo de evento já existe.");
                return Page(); 
            }


            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cria o tipo de evento se não houver erros
            await _tipoEventoAppService.CreateAsync(TipoEvento);
            return NoContent();
        }
    }
}
