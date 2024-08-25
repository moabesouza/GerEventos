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

        public EventoAppService(IRepository<Evento, Guid> repository)
            : base(repository)
        {
            _eventoRepository = repository;

            GetPolicyName = GerEventosPermissions.Evento.Default;
            GetListPolicyName = GerEventosPermissions.Evento.Default;
            CreatePolicyName = GerEventosPermissions.Evento.Create;
            UpdatePolicyName = GerEventosPermissions.Evento.Edit;
            DeletePolicyName = GerEventosPermissions.Evento.Delete;
        }

     
        public override async Task<PagedResultDto<EventoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var query = await _eventoRepository
                .WithDetails(e => e.BalcaoVendas, e => e.TipoEvento, e => e.Produtor) 
                .OrderBy(e => e.Nome)
                .ToListAsync();

            var totalCount = query.Count;

            var eventos = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(e => ObjectMapper.Map<Evento, EventoDto>(e))
                .ToList();

            return new PagedResultDto<EventoDto>(
                totalCount,
                eventos
            );
        }
    }
}
