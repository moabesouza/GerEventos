using System;
using System.Threading.Tasks;
using GerEventos.Services.TipoEventos;
using GerEventos.Services.Dtos.TipoEventos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using GerEventos.Services.Dtos.TipoEvento;
using Volo.Abp.ObjectMapping;

namespace GerEventos.Pages.TipoEventos
{
    public class EditModalModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateTipoEventoDto TipoEvento { get; set; }

        private readonly ITipoEventoAppService _tipoEventoAppService;

        public EditModalModel(ITipoEventoAppService tipoEventoAppService)
        {
            _tipoEventoAppService = tipoEventoAppService;
        }

        public async Task OnGetAsync()
        {
            var tipoEventoDto = await _tipoEventoAppService.GetAsync(Id);
            TipoEvento = ObjectMapper.Map<TipoEventoDto, CreateUpdateTipoEventoDto>(tipoEventoDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _tipoEventoAppService.UpdateAsync(Id, TipoEvento);
            return NoContent();
        }
    }
}
