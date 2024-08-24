using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using GerEventos.Services.Dtos.TipoEventos;
using GerEventos.Entities;
using GerEventos.Services.Dtos.TipoEvento;
using System.Threading.Tasks;

namespace GerEventos.Services.TipoEventos
{
    public interface ITipoEventoAppService :
        ICrudAppService< // Defines CRUD methods
            TipoEventoDto, // Used to show event types
            Guid, // Primary key of the event type entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateTipoEventoDto> // Used to create/update an event type
    {
        Task DeactivateAsync(Guid id);
        Task ActivateAsync(Guid id);
        Task<bool> nomeJaExiste(string nome);

    }
}
