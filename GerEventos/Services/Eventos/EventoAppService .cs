using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerEventos.Entities;
using GerEventos.Services.Dtos.Eventos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Permissions;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using GerEventos.Services.Dtos.Produtor;

namespace GerEventos.Services.Eventos
{
    public class EventoAppService :
        CrudAppService<
            Evento, // A entidade Evento
            EventoDto, // Usado para mostrar eventos
            Guid, // Chave primária da entidade evento
            PagedAndSortedResultRequestDto, // Usado para paginação/ordenamento
            CreateUpdateEventoDto>, // Usado para criar/atualizar um evento
        IEventoAppService // Implementa a IEventoAppService
    {
        private readonly IRepository<Evento, Guid> _eventoRepository;
        private readonly IRepository<Produtor> _produtorRepository;

        public EventoAppService(IRepository<Evento, Guid> repository,
                                IRepository<Produtor> produtorRepository)
            : base(repository)
        {
            _eventoRepository = repository;
            _produtorRepository = produtorRepository ?? throw new ArgumentNullException(nameof(produtorRepository));

            GetPolicyName = GerEventosPermissions.Evento.Default;
            GetListPolicyName = GerEventosPermissions.Evento.Default;
            CreatePolicyName = GerEventosPermissions.Evento.Create;
            UpdatePolicyName = GerEventosPermissions.Evento.Edit;
            DeletePolicyName = GerEventosPermissions.Evento.Delete;
        }

        public override async Task<PagedResultDto<EventoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = _eventoRepository
                .WithDetails(e => e.BalcaoVendas, e => e.TipoEvento, e => e.Produtor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }

            var totalCount = await query.CountAsync();

            var eventos = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var eventosDto = eventos.Select(e => ObjectMapper.Map<Evento, EventoDto>(e)).ToList();

            return new PagedResultDto<EventoDto>(
                totalCount,
                eventosDto
            );
        }

        public async Task<PagedResultDto<EventoDto>> GetListFilterAsync(FilterEventoDto input)
        {
            var query = _eventoRepository
                .WithDetails(e => e.BalcaoVendas, e => e.TipoEvento, e => e.Produtor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(input.Nome))
            {
                query = query.Where(e => e.Nome.Contains(input.Nome));
            }

            if (input.ProdutorId.HasValue)
            {
                query = query.Where(e => e.ProdutorId == input.ProdutorId.Value);
            }

            if (input.DataInicio.HasValue && input.DataFim.HasValue)
            {
                var dataInicio = input.DataInicio.Value.Date;
                var dataFim = input.DataFim.Value.Date.AddDays(1).AddTicks(-1); // Incluir o último momento do dia final

                query = query.Where(e =>
                    (e.DataInicio <= dataFim) && // Evento inicia antes ou no último momento do período
                    (e.DataFim >= dataInicio)); // Evento termina após ou no primeiro momento do período
            }
            else if (input.DataInicio.HasValue)
            {
                var dataInicio = input.DataInicio.Value.Date;

                query = query.Where(e =>
                    e.DataFim >= dataInicio); // Eventos que terminam após a data de início
            }
            else if (input.DataFim.HasValue)
            {
                var dataFim = input.DataFim.Value.Date.AddDays(1).AddTicks(-1);

                query = query.Where(e =>
                    e.DataInicio <= dataFim); // Eventos que começam antes ou no último momento da data final
            }

            // Aplicar ordenação dinâmica
            if (!string.IsNullOrEmpty(input.Sorting))
            {
                var sortingParts = input.Sorting.Split(' ');
                var sortField = sortingParts[0];
                var sortDirection = sortingParts.Length > 1 ? sortingParts[1] : "asc";

                if (sortField.Equals("nomeProdutor", StringComparison.OrdinalIgnoreCase))
                {
                    query = sortDirection.ToLower() == "asc"
                        ? query.OrderBy(e => e.Produtor.Nome)
                        : query.OrderByDescending(e => e.Produtor.Nome);
                }
                else if (sortField.Equals("nomeTipoEvento", StringComparison.OrdinalIgnoreCase))
                {
                    query = sortDirection.ToLower() == "asc"
                        ? query.OrderBy(e => e.TipoEvento.Nome)
                        : query.OrderByDescending(e => e.TipoEvento.Nome);
                }
                else
                {
                    query = query.OrderBy(input.Sorting);
                }
            }

            var totalCount = await query.CountAsync();

            var eventos = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var eventosDto = eventos.Select(e => ObjectMapper.Map<Evento, EventoDto>(e)).ToList();

            return new PagedResultDto<EventoDto>(
                totalCount,
                eventosDto
            );
        }








    }
}
