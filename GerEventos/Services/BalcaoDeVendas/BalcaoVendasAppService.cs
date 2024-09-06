using System;
using GerEventos.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Services.Dtos.BalcaoVendas;
using GerEventos.Entities;

namespace GerEventos.Services.BalcaoDeVendas
{
    public class BalcaoVendasAppService :
        CrudAppService<
            BalcaoVendas, // A entidade BalcaoVendas
            BalcaoVendasDto, // Usado para mostrar balcões de vendas
            Guid, // Chave primária da entidade balcão de vendas
            PagedAndSortedResultRequestDto, // Usado para paginação/ordenamento
            CreateUpdateBalcaoVendasDto>, // Usado para criar/atualizar um balcão de vendas
        IBalcaoVendasAppService // Implementa a IBalcaoDeVendasAppService
    {
        public BalcaoVendasAppService(IRepository<BalcaoVendas, Guid> repository)
            : base(repository)
        {
            GetPolicyName = GerEventosPermissions.BalcaoVendas.Default;
            GetListPolicyName = GerEventosPermissions.BalcaoVendas.Default;
            CreatePolicyName = GerEventosPermissions.BalcaoVendas.Create;
            UpdatePolicyName = GerEventosPermissions.BalcaoVendas.Edit;
        }

        public async Task DeactivateAsync(Guid id)
        {
            var balcaoVendas = await Repository.GetAsync(id);
            balcaoVendas.Status = StatusEnum.Desativado;
            await Repository.UpdateAsync(balcaoVendas);
        }

        public async Task ActivateAsync(Guid id)
        {
            var balcaoVendas = await Repository.GetAsync(id);
            balcaoVendas.Status = StatusEnum.Ativado;
            await Repository.UpdateAsync(balcaoVendas);
        }

        public async Task<bool> nomeJaExiste(string nome)
        {
            return await Repository.AnyAsync(te => te.Nome == nome);
        }

    }
}
