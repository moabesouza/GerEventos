using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Entities;

namespace GerEventos.Services.Estatistica
{
    public class EventoEstatisticasAppService : ApplicationService, IEventoEstatisticasAppService
    {
        private readonly IRepository<Evento, Guid> _eventoRepository;

        public EventoEstatisticasAppService(IRepository<Evento, Guid> eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<List<int>> GetEventosPorMesAsync(int ano)
        {
            var queryable = await _eventoRepository.GetQueryableAsync();

            var eventosPorMes = await queryable
                .Where(e => e.DataInicio.Year == ano)
                .GroupBy(e => e.DataInicio.Month)
                .Select(g => new { Mes = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            var resultado = new List<int>(new int[12]);

            foreach (var item in eventosPorMes)
            {
                resultado[item.Mes - 1] = item.Quantidade;
            }

            return resultado;
        }

        public async Task<Dictionary<string, int>> GetEventosPorTipoAsync()
        {
            var queryable = await _eventoRepository.GetQueryableAsync();

            return await queryable
                .GroupBy(e => e.TipoEvento.Nome)
                .Select(g => new { Tipo = g.Key, Quantidade = g.Count() })
                .ToDictionaryAsync(g => g.Tipo, g => g.Quantidade);
        }
    }
}
