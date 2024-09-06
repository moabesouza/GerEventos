using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerEventos.Services.Dtos.Eventos;
using GerEventos.Services.Eventos;
using GerEventos.Services.TipoEventos;
using GerEventos.Services.BalcaoDeVendas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Application.Dtos;
using GerEventos.Entities;
using GerEventos.Services.Produtores;

namespace GerEventos.Pages.Eventos
{
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty]
        public CreateUpdateEventoDto Evento { get; set; } = new CreateUpdateEventoDto();
        public List<SelectListItem> TipoEventos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BalcaoVendas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Produtores { get; set; } = new List<SelectListItem>();

        private readonly IEventoAppService _eventoAppService;
        private readonly ITipoEventoAppService _tipoEventoAppService;
        private readonly IBalcaoVendasAppService _balcaoVendasAppService;
        private readonly IProdutorAppService _produtorAppService;

        public CreateModalModel(
            IEventoAppService eventoAppService,
            ITipoEventoAppService tipoEventoAppService,
            IBalcaoVendasAppService balcaoVendasAppService,
            IProdutorAppService produtorAppService)
        {
            _produtorAppService = produtorAppService;
            _eventoAppService = eventoAppService;
            _tipoEventoAppService = tipoEventoAppService;
            _balcaoVendasAppService = balcaoVendasAppService;
        }

        public async Task OnGetAsync()
        {
            var requestDto = new PagedAndSortedResultRequestDto();

            var tipoEventosResult = await _tipoEventoAppService.GetListAsync(requestDto);
            var balcaoVendasResult = await _balcaoVendasAppService.GetListAsync(requestDto);
            var produtoresResult = await _produtorAppService.GetListAsync(requestDto);

            TipoEventos = tipoEventosResult.Items
                .Where(t => t.Status == StatusEnum.Ativado)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nome
                })
                .ToList();

            BalcaoVendas = balcaoVendasResult.Items
                .Where(b => b.Status == StatusEnum.Ativado)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Nome
                })
                .ToList();


            Produtores = produtoresResult.Items
                .Where(b => b.Status == StatusEnum.Ativado)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Nome
                })
                .ToList();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                return Page();
            }

            try
            {
                await _eventoAppService.CreateAsync(Evento);
                return NoContent();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o evento.");
                return Page();
            }
        }
    }
}
