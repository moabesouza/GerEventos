using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerEventos.Services.Dtos.Eventos;
using GerEventos.Services.Eventos;
using GerEventos.Services.TipoEventos;
using GerEventos.Services.BalcaoVendas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Pages.Eventos
{
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty]
        public CreateUpdateEventoDto Evento { get; set; } = new CreateUpdateEventoDto();
        public List<SelectListItem> TipoEventos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BalcaoVendas { get; set; } = new List<SelectListItem>();

        private readonly IEventoAppService _eventoAppService;
        private readonly ITipoEventoAppService _tipoEventoAppService;
        private readonly IBalcaoVendasAppService _balcaoVendasAppService;

        public CreateModalModel(
            IEventoAppService eventoAppService,
            ITipoEventoAppService tipoEventoAppService,
            IBalcaoVendasAppService balcaoVendasAppService)
        {
            _eventoAppService = eventoAppService;
            _tipoEventoAppService = tipoEventoAppService;
            _balcaoVendasAppService = balcaoVendasAppService;
        }

        public async Task OnGetAsync()
        {
            var requestDto = new PagedAndSortedResultRequestDto();

            var tipoEventosResult = await _tipoEventoAppService.GetListAsync(requestDto);
            var balcaoVendasResult = await _balcaoVendasAppService.GetListAsync(requestDto);

            TipoEventos = tipoEventosResult.Items.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nome
            }).ToList();

            BalcaoVendas = balcaoVendasResult.Items.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Nome
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _eventoAppService.CreateAsync(Evento);
            return NoContent();
        }
    }
}
