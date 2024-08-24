using System;
using GerEventos.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Services.Dtos.Eventos;
using GerEventos.Entities;

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
        public EventoAppService(IRepository<Evento, Guid> repository)
            : base(repository)
        {
            GetPolicyName = GerEventosPermissions.Evento.Default;
            GetListPolicyName = GerEventosPermissions.Evento.Default;
            CreatePolicyName = GerEventosPermissions.Evento.Create;
            UpdatePolicyName = GerEventosPermissions.Evento.Edit;
            DeletePolicyName = GerEventosPermissions.Evento.Delete;
        }
    }
}
