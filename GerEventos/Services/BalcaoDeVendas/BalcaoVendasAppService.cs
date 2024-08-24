using System;
using GerEventos.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Services.Dtos.BalcaoVendas;
using GerEventos.Entities;
using GerEventos.Services.BalcaoVendas;

namespace GerEventos.Services.BalcaoVendas
{
    public class BalcaoVendasAppService :
        CrudAppService<
            global::BalcaoVendas, // A entidade BalcaoVendas
            BalcaoVendasDto, // Usado para mostrar balcões de vendas
            Guid, // Chave primária da entidade balcão de vendas
            PagedAndSortedResultRequestDto, // Usado para paginação/ordenamento
            CreateUpdateBalcaoVendasDto>, // Usado para criar/atualizar um balcão de vendas
        IBalcaoVendasAppService // Implementa a IBalcaoDeVendasAppService
    {
        public BalcaoVendasAppService(IRepository<global::BalcaoVendas, Guid> repository)
            : base(repository)
        {
            GetPolicyName = GerEventosPermissions.BalcaoVendas.Default;
            GetListPolicyName = GerEventosPermissions.BalcaoVendas.Default;
            CreatePolicyName = GerEventosPermissions.BalcaoVendas.Create;
            UpdatePolicyName = GerEventosPermissions.BalcaoVendas.Edit;
            DeletePolicyName = GerEventosPermissions.BalcaoVendas.Delete;
        }

    }
}
