using System;
using GerEventos.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Services.Dtos.TipoEventos;
using GerEventos.Entities;
using GerEventos.Services.Dtos.TipoEvento;

namespace GerEventos.Services.TipoEventos
{
    public class TipoEventoAppService :
        CrudAppService<
            TipoEvento, // A entidade TipoEvento
            TipoEventoDto, // Usado para mostrar tipos de eventos
            Guid, // Chave primária da entidade tipo de evento
            PagedAndSortedResultRequestDto, // Usado para paginação/ordenamento
            CreateUpdateTipoEventoDto>, // Usado para criar/atualizar um tipo de evento
        ITipoEventoAppService // Implementa a ITipoEventoAppService
    {
        public TipoEventoAppService(IRepository<TipoEvento, Guid> repository)
            : base(repository)
        {
            GetPolicyName = GerEventosPermissions.TipoEvento.Default;
            GetListPolicyName = GerEventosPermissions.TipoEvento.Default;
            CreatePolicyName = GerEventosPermissions.TipoEvento.Create;
            UpdatePolicyName = GerEventosPermissions.TipoEvento.Edit;
        }

        public async Task DeactivateAsync(Guid id)
        {
            var tipoEvento = await Repository.GetAsync(id);
            tipoEvento.Status = StatusEnum.Desativado;
            await Repository.UpdateAsync(tipoEvento); 
        }

        public async Task ActivateAsync(Guid id)
        {
            var tipoEvento = await Repository.GetAsync(id); 
            tipoEvento.Status = StatusEnum.Ativado; 
            await Repository.UpdateAsync(tipoEvento);
        }


    }
}
