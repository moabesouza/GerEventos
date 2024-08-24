using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using GerEventos.Services.Dtos.BalcaoVendas;
using GerEventos.Entities;
using GerEventos.Services.Dtos.BalcaoVendas;

namespace GerEventos.Services.BalcaoVendas
{
    public interface IBalcaoVendasAppService :
    ICrudAppService< // Defines CRUD methods
            BalcaoVendasDto, // Used to show sales counters
            Guid, // Primary key of the sales counter entity
            PagedAndSortedResultRequestDto, // Used for paging/sorting
            CreateUpdateBalcaoVendasDto> // Used to create/update a sales counter
    {
        Task DeactivateAsync(Guid id);
        Task ActivateAsync(Guid id);
        Task<bool> nomeJaExiste(string nome);

    }
}
