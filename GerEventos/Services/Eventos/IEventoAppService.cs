using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using GerEventos.Services.Dtos.Eventos;
using GerEventos.Entities;

namespace GerEventos.Services.Eventos
{
    public interface IEventoAppService :
        ICrudAppService< // Defines CRUD methods
            EventoDto, // Used to show events
            Guid, // Primary key of the event entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateEventoDto> // Used to create/update an event
    {
    }
}
