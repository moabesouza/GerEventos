using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GerEventos.Services;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using GerEventos.Services.Contador;
using GerEventos.Services.Estatistica;

namespace GerEventos.Pages
{
    public class IndexModel : AbpPageModel
    {
        private readonly IContadorAppService _contadorAppService;
        private readonly IEventoEstatisticasAppService _estatisticasAppService;

        public IndexModel(IContadorAppService contadorAppService, IEventoEstatisticasAppService estatisticasAppService)
        {
            _contadorAppService = contadorAppService;
            _estatisticasAppService = estatisticasAppService;
        }

        public int EventCount { get; set; }
        public int BalcaoCount { get; set; }
        public int ProdutorCount { get; set; }
        public int[] EventosPorMes { get; set; }
        public Dictionary<string, int> EventosPorTipo { get; set; }

        public async Task OnGetAsync()
        {
            // Obtendo contadores
            EventCount = await _contadorAppService.GetEventCountAsync();
            BalcaoCount = await _contadorAppService.GetBalcaoCountAsync();
            ProdutorCount = await _contadorAppService.GetProdutorCountAsync();

            // Obtendo estatísticas
            var eventosPorMes = await _estatisticasAppService.GetEventosPorMesAsync(DateTime.Now.Year);
            EventosPorMes = eventosPorMes.ToArray();
            EventosPorTipo = await _estatisticasAppService.GetEventosPorTipoAsync();
        }
    }
}
