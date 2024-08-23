using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerEventos.Services.Eventos;
using GerEventos.Services.Dtos.Eventos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using GerEventos.Services.BalcaoVendas;
using GerEventos.Services.TipoEventos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;

namespace GerEventos.Pages.Eventos
{
    public class EditModalModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateEventoDto Evento { get; set; }
        public List<SelectListItem> TipoEventos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BalcaoVendas { get; set; } = new List<SelectListItem>();

        private readonly IEventoAppService _eventoAppService;
        private readonly ITipoEventoAppService _tipoEventoAppService;
        private readonly IBalcaoVendasAppService _balcaoVendasAppService;

        public EditModalModel(
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
            var tipoEventosResult = await _tipoEventoAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            TipoEventos = tipoEventosResult.Items.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nome
            }).ToList();
         
            var balcaoVendasResult = await _balcaoVendasAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            BalcaoVendas = balcaoVendasResult.Items.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Nome
            }).ToList();

            var eventoDto = await _eventoAppService.GetAsync(Id);
            Evento = ObjectMapper.Map<EventoDto, CreateUpdateEventoDto>(eventoDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _eventoAppService.UpdateAsync(Id, Evento);
            return NoContent();
        }
    }
}
